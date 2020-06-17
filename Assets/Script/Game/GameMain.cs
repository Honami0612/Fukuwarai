using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
//using UnityEngine.Networking;
//no need

public class GameMain : MonoBehaviour
{

    [SerializeField]
    GameObject ojisanhair;

    PhotonView photonView;

    [SerializeField]
    GameObject partsPrefab;

    public int num = 0;

    public string num_string = "0";


    public int count=0;
    public string count_string = "0";

    private bool posManagement = false;

    [SerializeField]
    GameObject screenshotPrefab;
    GameObject screenshot;

    [SerializeField]
    GameObject[] kao;

    // name と　ファイル名　同じ
    [SerializeField]
    GameObject[] partLoad;

    [SerializeField]
    List<Animation> partsAnimation = new List<Animation>();


    // 今投げるパーツ
    public GameObject nowParts;
    private bool shootFlag = false;
    
    string[] folder = { "1ojisan", "2man", "3apple", "4moon", "5rabbit" };

    [SerializeField]
    MouseController mouse;

    [SerializeField]
    List<object> id_viewId = new List<object>();

    [PunRPC]//生成したパーツ格納
    public List<GameObject> instatiateParts = new List<GameObject>();
    [PunRPC]
    public List<GameObject> faceinParts = new List<GameObject>();

    public int playerId = 1;
    private string playerId_string =  "";

    [SerializeField]
    Text myTurn;

    [SerializeField]
    GameObject finish_button;
    

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.OnEventCall += OnRaiseEvent;
       
    }

   


    private void OnRaiseEvent(byte code, object g, int senderid)
    {
        string data = g.ToString();
        string tmp = null;
        int id = 0, viewId = 0;

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == 10)
            {
                id = int.Parse(tmp);
                tmp = null;
            }
            else
            {
                tmp += data[i];
            }
        }
        viewId = int.Parse(tmp);

        if (PhotonView.Find(viewId) == null)
        {
            StartGenerate(id, viewId);
        }


    }

    private void Start()
    {

        count = 0;
        //Debug.LogWarning("ID:"+PhotonNetwork.player.ID);//player特定
        //Debug.LogWarning("Length:"+PhotonNetwork.playerList.Length);//player人数

        finish_button.SetActive(false);

        int faceselectnumber = FaceSelect.SelectNumber - 1;
        Instantiate(kao[faceselectnumber]);//顔生成
        Debug.Log("faceselectnumber:"+faceselectnumber);

        ojisanhair.gameObject.SetActive(false);
        if (faceselectnumber == 0)
        {
            ojisanhair.gameObject.SetActive(true);
        }


        if (GameObject.Find("ScreenShot(Clone)") == null)
        {
            screenshot = Instantiate(screenshotPrefab);
        }
        else
        {
            screenshot = GameObject.Find("ScreenShot(Clone)");
        }


        partLoad = Resources.LoadAll<GameObject>("Game/" + folder[faceselectnumber]); //呼び出し一括

        int test = PhotonNetwork.player.ID - 1;

        object[] t = new object[]
        {
            test,PhotonNetwork.AllocateViewID()

        };

        photonView.RPC("StartGenerate", PhotonTargets.AllBuffered, t);

        TurnChange();


    }



    void Update()
    {
        TurnChange();
        if (PhotonNetwork.isNonMasterClientInRoom != true)
        {
            for (int i = 0; i < id_viewId.Count; i++) PhotonNetwork.RaiseEvent(1, id_viewId[i], true, RaiseEventOptions.Default);
        }

        //MoveScript.OnTriggerでWakuに入ったらposManagement
        //master
        if (posManagement)
        {
            posManagement = false;
            Invoke("PosStop", 1.2f);
        }

        if (Input.GetKeyDown(KeyCode.A)) num_string += 1.ToString();

        //画面からパーツを出さないようにしたい。でもこれでは枠に当たったとき、どれにどのように力加える？
        //for(int h = 0; h < instatiateParts.Count; h++)
        //{
        //    if (instatiateParts[h].transform.position.x < mouse.LeftBottom.x)
        //    {

        //    }
        //}
        
    }



    [PunRPC]//同期
    public void StartGenerate(int id, int view)
    {
        //生成できるパーツが残っているなら
        if (instatiateParts.Count < partLoad.Length)
        {
            GameObject stobj = Instantiate(partLoad[id]);
            stobj.transform.localPosition = new Vector3(0, -11.4f, 0);
            stobj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            stobj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            stobj.GetComponent<PhotonView>().viewID = view;
            stobj.SetActive(false);
            bool d = stobj.GetComponent<PhotonView>().isMine;
            if (d)
            {
                nowParts = stobj;
                object g = id + "\n" + view;
                id_viewId.Add(g);
                stobj.SetActive(true);
                stobj.GetComponent<MoveScript>().Mine = true;
                photonView.RPC("Num", PhotonTargets.MasterClient);

            }

            instatiateParts.Add(stobj);
            shootFlag = false;
        }
        //残っていないなら
        else
        {
            Debug.Log("待つ");
            if (photonView.isMine) photonView.RPC("Count", PhotonTargets.MasterClient);

            if (count == PhotonNetwork.playerList.Length) finish_button.SetActive(true);
        }
    }


    public void OnClick_finish_button()
    {
        photonView.RPC("GoFinish", PhotonTargets.All);
    }

    [PunRPC]
    void GoFinish()
    {
        SceneManager.LoadScene("Finish");
    }


    public void TurnChange()
    {
        if(playerId == PhotonNetwork.player.ID)
        {
            if (!shootFlag)
            {
                shootFlag = true;
                myTurn.text = "あなたの番です";
                mouse.ResetData();
                mouse.SetParts(nowParts.GetComponent<MoveScript>());
            }
        }
        else
        {
            myTurn.text = "他の人の番です";
        }
    }

    [PunRPC]
    void Count()
    {
        count++;
    }

    // Clientがマスターに呼ばせる

    [PunRPC]
    void Num()
    {
        num++;
    }


    [PunRPC]
    void ID()
    {
        Debug.Log("現在のID:" + playerId);
        playerId = playerId + 1;
        if (playerId  > PhotonNetwork.playerList.Length)
        {
            playerId = 1;
        }
        Debug.Log("IDをmasterが足す:" + playerId);
    }


    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            num_string = num.ToString();

            playerId_string = playerId.ToString();
            count_string = count.ToString();
            stream.SendNext(num_string);
            stream.SendNext(playerId_string);
            stream.SendNext(count_string);
            Debug.LogError("書き込み");
        }
        else//読み込み処理
        {
            num_string = (string)stream.ReceiveNext();

            playerId_string = (string)stream.ReceiveNext();
            count_string = (string)stream.ReceiveNext();
            num = int.Parse(num_string);
            playerId = int.Parse(playerId_string);
            count = int.Parse(count_string);

            Debug.LogError("読み込み");
        }
    }





    IEnumerator timestop()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Finish");
    }



    void PosStop()
    {
        foreach (var k in faceinParts) k.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < faceinParts.Count; i++) faceinParts[i].GetComponent<Rigidbody>().isKinematic = false;
    }


    [PunRPC]
    public void SetActivechange(int viewId)
    {
        foreach (GameObject change in instatiateParts)
        {
            if (change.GetComponent<PhotonView>().viewID == viewId) change.SetActive(true);
        }
    }

    public void FaceinAdd(GameObject gameObject)
    {

        faceinParts.Add(gameObject);
    }


    public bool management
    {
        get { return posManagement; }
        set { posManagement = value; }
    }

    public int nowNum
    {
        get { return num; }
        set { num = value; }
    }

}


/*
 *生成したパーツはaにリストとして格納
 *輪郭内に入ったパーツをpartにリストとして格納　MoveScriptのOnTriggerか？
 *partに格納したものをGameaMainのPosStop()とTriggerfalse()で止める
 **Wakuが機能してないのを確認
 */

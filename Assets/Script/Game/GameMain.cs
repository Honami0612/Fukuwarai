using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class GameMain : MonoBehaviour
{
    PhotonView photonView;

    private int faceselectnumber;
    [SerializeField]
    GameObject ojisanhair;
    [SerializeField]
    GameObject partsPrefab;
    [SerializeField]
    GameObject[] kao;
    private  GameObject[] partLoad;
    string[] folder = { "1ojisan", "2man", "3apple", "4moon", "5rabbit" };
    [PunRPC]//生成したパーツ格納
    public List<GameObject> instatiateParts = new List<GameObject>();
    [PunRPC]
    private List<GameObject> faceinParts = new List<GameObject>();
    [SerializeField]
    List<Animation> partsAnimation = new List<Animation>();
    [SerializeField]
    GameObject nowParts;
    private bool shootFlag = false;

    private int num = 0;
    private string numString = "0";
    private int count=0;
    private string countString = "0";
    private bool posManagement = false;

    [SerializeField]
    List<object> idViewId = new List<object>();
    public int playerId = 1;
    private string playerIdString =  "";

    [SerializeField]
    Text myTurn;
    [SerializeField]
    Text operationText;

    [SerializeField]
    GameObject finishButton;

    [SerializeField]
    MouseController mouseController;

    //修正中
    [SerializeField]
    GameObject screenshotPrefab;
    private GameObject screenshot;

    private Transform nowPartTransform;



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
        if (PhotonView.Find(viewId) == null) StartGenerate(id, viewId);
       
    }



    private void Start()
    {

        count = 0;

        finishButton.SetActive(false);
        faceselectnumber = FaceSelect.SelectNumber - 1;
        Instantiate(kao[faceselectnumber]);
        ojisanhair.gameObject.SetActive(false);

        if (faceselectnumber == 0) ojisanhair.gameObject.SetActive(true);
        if (GameObject.Find("ScreenShot(Clone)") == null)
        {
            screenshot = Instantiate(screenshotPrefab);
        }
        else
        {
            screenshot = GameObject.Find("ScreenShot(Clone)");
        }

        partLoad = Resources.LoadAll<GameObject>("Game/" + folder[faceselectnumber]); 
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
            for (int i = 0; i < idViewId.Count; i++) PhotonNetwork.RaiseEvent(1, idViewId[i], true, RaiseEventOptions.Default);
        }

        if (posManagement) //MoveScript.OnTrigger
        {
            posManagement = false;
            Invoke("PosStop", 1.2f);
        }
    }



    [PunRPC]
    public void StartGenerate(int id, int view)
    { 
        if (instatiateParts.Count < partLoad.Length)//生成できるパーツが残っているなら
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
                idViewId.Add(g);
                stobj.SetActive(true);
                stobj.GetComponent<MoveScript>().Mine = true;
                photonView.RPC("Num", PhotonTargets.MasterClient);
            }
            instatiateParts.Add(stobj);
            shootFlag = false;
        }
        else //残っていないなら待機
        {
            if (photonView.isMine) photonView.RPC("Count", PhotonTargets.MasterClient);
            if (count == PhotonNetwork.playerList.Length)
			{
				finishButton.SetActive(true);
                photonView.RPC("CommentOut", PhotonTargets.All);
            }


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
                mouseController.ResetData();
                mouseController.SetParts(nowParts.GetComponent<MoveScript>());
                nowPartTransform = nowParts.GetComponent<Transform>();
            }
        }
        else
        {
            myTurn.text = "他の人の番です";
        }
    }

    [PunRPC]
    void CommentOut()
    {
        Debug.Log("CommentOUtに入りました");
        operationText.gameObject.SetActive(false);
        myTurn.gameObject.SetActive(false);
    }


    [PunRPC]
    void Count()
    {
        count++;
    }

   
    [PunRPC]
    void Num()
    {
        num++;
    }


    [PunRPC]
    void ID()
    {
        playerId = playerId + 1;
        if (playerId  > PhotonNetwork.playerList.Length)
        {
            playerId = 1;
        }
    }


    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)//書き込み処理
        {
            numString = num.ToString();

            playerIdString = playerId.ToString();
            countString = count.ToString();
            stream.SendNext(numString);
            stream.SendNext(playerIdString);
            stream.SendNext(countString);
        }
        else//読み込み処理
        {
            numString = (string)stream.ReceiveNext();

            playerIdString = (string)stream.ReceiveNext();
            countString = (string)stream.ReceiveNext();
            num = int.Parse(numString);
            playerId = int.Parse(playerIdString);
            count = int.Parse(countString);
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
        set { posManagement = value; }
    }

    public int nowNum
    {
        get { return num; }
    }

    public int nowCount
    {
        get { return count; }
        set { count = value; }
    }

    public Transform SetnowPartTransform
    {
        get { return nowPartTransform; }
    }

}


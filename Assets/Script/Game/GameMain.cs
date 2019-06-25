using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
//using UnityEngine.Networking;

public class GameMain : MonoBehaviour {

    PhotonView photonView; 

    [SerializeField]
    GameObject partsPrefab;

	[PunRPC]
    private int num = 0;

   // float time = 10.0f;

    [PunRPC]
	private bool posmanagement = false;

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

	[SerializeField]//Wakuを超えたパーツ格納
	List<GameObject> parts = new List<GameObject>();

	string[] folder={"1ojisan","2man","3apple","4moon","5rabbit"};



    [SerializeField]
    MouseController mouse;

    [SerializeField]
    List<object> id_viewId = new List<object>();

    //衝突 - isKinematic ON/OFF parts
    [PunRPC]//生成したパーツ格納
    public List<GameObject> a = new List<GameObject>();

    [SerializeField]
    Text message1;
    [SerializeField]
    Text message2;

 

    private void Awake()
    {
       
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents = new List<Component>();
        PhotonNetwork.OnEventCall += OnRaiseEvent;

    }

    private void OnRaiseEvent(byte code,object g,int senderid)
    {
        string data = g.ToString();
        string tmp = null;
        int id = 0, viewId = 0;

        for(int i = 0; i < data.Length; i++)
        {
            if(data[i] == 10)
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

        Debug.LogWarning("id =" + id);
        Debug.LogError("viewId = " + viewId);
        if (PhotonView.Find(viewId) == null)
        {
            StartGenerate(id, viewId);
        }


    }

    private void Start()
    {
		int faceselectnumber = FaceSelect.SelectNumber - 1;
		//Debug.Log (faceselectnumber);
		Instantiate (kao[faceselectnumber]);//顔生成

        if (GameObject.Find("ScreenShot(Clone)")==null)
        {
            screenshot = Instantiate(screenshotPrefab);
        }
        else
        {
            screenshot = GameObject.Find("ScreenShot(Clone)"); 
        }

       
        partLoad = Resources.LoadAll <GameObject> ("Game/"+folder[faceselectnumber]); //呼び出し一括

        int test = PhotonNetwork.player.ID - 1;

        object[] t = new object[]
        {
            test,PhotonNetwork.AllocateViewID()

        };

     

        photonView.RPC("StartGenerate", PhotonTargets.AllBuffered,t);

     

        GameObject.Find("arrowArea").GetComponent<MouseController>().SetParts(a[test].GetComponent<MoveScript>());
        //a[test].GetComponent<MoveScript>().Mine=true;



    
    }
   



    void Update()
    {
        if (PhotonNetwork.isNonMasterClientInRoom != true)
        {
            for (int i = 0; i < id_viewId.Count; i++)
            {
                PhotonNetwork.RaiseEvent(1, id_viewId[i], true, RaiseEventOptions.Default);
            }
        }


        //master
        if (posmanagement == true)
        {
            posmanagement = false;
            Invoke("PosStop", 1.2f);
        }

     
    }



    [PunRPC]
    public void StartGenerate(int id,int view)
    {
        if (a.Count < partLoad.Length)
        {

            message1.text += "StartGeneのid=" + id.ToString() + "/view=" + view.ToString();
            GameObject stobj = Instantiate(partLoad[id]);
            stobj.transform.localPosition = new Vector3(0, -11.4f, 0);
            stobj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            stobj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            stobj.GetComponent<PhotonView>().viewID = view;
            stobj.SetActive(false);
            bool d = stobj.GetComponent<PhotonView>().isMine;
            if (d)
            {
                object g = id + "\n" + view;
                id_viewId.Add(g);
                stobj.SetActive(true);
                stobj.GetComponent<MoveScript>().Mine = true;
            }
            a.Add(stobj);

        }
        else
        {
            Debug.Log("待つ");
        }
    }


    [PunRPC]
    public void Generate()
	{
		Debug.Log ("確認");

		if (a.Count < partLoad.Length) {

            GameObject obj = Instantiate(partLoad[num]);
            obj.transform.localPosition = new Vector3(0, -11.4f, 0);
            obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            obj.SetActive(false);
            if (photonView.isMine)
            {
                obj.SetActive(true);
                num++;
            }
            a.Add(obj);


        } else {
			Debug.Log ("###");
           // message.text = "他のプレイヤーを待つ";
		
            //大事！スクショマスターのみ	//screenshot.GetComponent<Screenshot> ().ScreenShotFlag = true;
			

		}
	}


    IEnumerator timestop()
    {
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Finish");
	}



    void PosStop()
	{
        foreach(var k in parts)
        {
            k.GetComponent<Rigidbody>().isKinematic = true;
        }
        /*
		for(int j=0;j<parts.Count;j++)
		{
			Debug.Log ("PosStop");
			parts[j].GetComponent<Rigidbody>().isKinematic = true;
		}
		Triggerfalse ();
        */
    }


	void Triggerfalse()
	{
		
		for(int i=0;i<parts.Count;i++)
        {
			Debug.Log ("Triggerfalse");

			parts[i].GetComponent<Rigidbody> ().isKinematic = false;
		}
	}


	public bool management 
    {
		get { return posmanagement; }
		set { posmanagement = value; }
	}
}


/*
 *生成したパーツはaにリストとして格納
 *輪郭内に入ったパーツをpartにリストとして格納　MoveScriptのOnTriggerか？
 *partに格納したものをGameaMainのPosStop()とTriggerfalse()で止める
 **Wakuが機能してないのを確認
 */
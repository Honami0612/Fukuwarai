using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
//using UnityEngine.Networking;

public class GameMain : MonoBehaviour {


    PhotonPlayer[] nowplayer = PhotonNetwork.playerList;
    PhotonView photonView; 

    [SerializeField]
    GameObject partsPrefab;

    [SerializeField]
    Text timerText;

	[PunRPC]
    private int num = 0;

    float time = 10.0f;

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

	[SerializeField]
	List<GameObject> parts = new List<GameObject>();

	string[] folder={"1ojisan","2man","3apple","4moon","5rabbit"};

    int now = 99;

    [SerializeField]
    MouseController mouse;

    //衝突 - isKinematic ON/OFF parts
    [PunRPC]
    public List<GameObject> a = new List<GameObject>();

    [SerializeField]
    Text message;

   /* void test()
    {
        // start
        //a.Add(PhotonNetwork.Instantiate(partsLoad[PhotonNetwork.player.ID - 1].name,new Vector3(0,0,0),Quaternion.identity,0));
        //mouse.SetParts(a[PhotonNetwork.player.ID - 1].GetComponent<MoveScript>());
        //now = PhotonNetwork.player.ID - 1;


        // awake master
        num = nowplayer.Length;


        // private
        if (a.Count < partsSprite.Length)
        {
            a.Add(PhotonNetwork.Instantiate(partsSprite[num].name, new Vector3(0, 0, 0), Quaternion.identity, 0));
            //mouse.SetParts(a[num].GetComponent<MoveScript>());
            //now = num;
            num++;
        }
        else
        {
            //待機
            // text =  "他のプレイヤーを待つ"
        }

    }p
    */   



    private void Awake()
    {
        num = nowplayer.Length;
        Debug.Log(num);
        photonView = GetComponent<PhotonView>();
        photonView.ObservedComponents = new List<Component>();

    }



    private void Start()
    {

        //num = 0;
		int faceselectnumber = FaceSelect.SelectNumber - 1;
		Debug.Log (faceselectnumber);
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
            test,
            PhotonNetwork.AllocateViewID()

        };

        photonView.RPC("StartGenerate", PhotonTargets.AllBuffered, t);
        message.text = PhotonNetwork.countOfPlayers.ToString();
     
               
       // a[PhotonNetwork.player.ID - 1].SetActive(true);
        GameObject.Find("arrowArea").GetComponent<MouseController>().SetParts(a[PhotonNetwork.player.ID - 1].GetComponent<MoveScript>());
        a[PhotonNetwork.player.ID - 1].GetComponent<MoveScript>().Mine=true;



        //for (int i = 0; i < nowplayer.Length; i++)
        //{
        //    Debug.Log("パーツ生成");
        //    a.Add(PhotonNetwork.Instantiate(partsLoad[i].name, new Vector3(0, -11.4f, 0), Quaternion.identity, 0));
        //    a[i].SetActive(true);
        //   // parts[i].transform.localPosition = new Vector3(0, -11.4f, 0);
        //    a[i].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        //    a[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
        //}


    }
   



    void Update()
    {

        //if (isLocalPlayer) {
        /*this.time -= Time.deltaTime;

        if (this.time < 0) {
            Generate ();
        } else {
            this.timerText.text = this.time.ToString ("F0");
        }*/
        //	} 

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
        GameObject stobj = Instantiate(partLoad[id]);
        stobj.transform.localPosition = new Vector3(0, -11.4f, 0);
        stobj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        stobj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        stobj.GetComponent<PhotonView>().viewID = view;
        stobj.SetActive(false);
        bool d = stobj.GetComponent<PhotonView>().isMine;
        Debug.Log(d);
        if (d)
        {
            stobj.SetActive(true);
        }
        a.Add(stobj);

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

            //以前/parts.Add (Instantiate (partsLoad [num]));　//パーツ生成
            // a.Add(PhotonNetwork.Instantiate(partsPrefab.name, new Vector3(0, -11.4f, 0), Quaternion.identity, 0));
            //以前/a[num].GetComponent<SpriteRenderer>().sprite = partsSprite[num];
            //parts [num].SetActive (true);
            //以前/parts [num].transform.localPosition = new Vector3 (0, -11.4f, 0);
            //parts [num].transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
            //以前/parts [num].transform.localRotation = Quaternion.Euler (0, 0, 0);

            ///this.time = 10.0f;
        } else {
			Debug.Log ("###");
            message.text = "他のプレイヤーを待つ";
		
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
		for(int j=0;j<parts.Count;j++)
		{
			Debug.Log ("PosStop");
			parts[j].GetComponent<Rigidbody>().isKinematic = true;
		}
		Triggerfalse ();

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


/* RPC 全プレイヤーがInstantiateで生成する。このままだと同期できないはず（要確認）
 *
 * 同期できなかった場合↓
 * 生成した後にPhotonViewつける　https://qiita.com/_karukaru_/items/f90fe4f269a72d2b46d0
 * これをつけることで同期設定をしている
 * これで動くかどうか確認
 * 生成はされるはず
 * 座標が変わるかどうか
 * 
 * 上記がうまくいけば
 * 変数・生成方法は変更しなくていい。
 * Instantiate時に自分のPlayer_Idを引数で送り、
 * RPCの最後で引数のPlayer_idと自分のPlayer_idが一致しているかを識別
 * 一致していない場合、今生成したパーツのSetActiveをfalseにする。
 * 一致していた場合、num++
 * そうすると生成場所は全プレイヤー同じであるが、見えていないため投げた時の影響はない。
 * 
 * 考えられるバグ
 * マウス操作を行った際に全パーツが動く
 * 非表示のものまで動くかもしれない（要確認）isMineでみてるから大丈夫かも？
 * 
 * 確認
 * Instatiateで生成した時のisMineは誰か確認する
 * RPCで呼んだ時も同様に
 */

    // isMine: 所有者
    //　生成者とは違う
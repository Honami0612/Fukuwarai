using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class MoveScript : MonoBehaviour
{

    private bool mine = false;



    private GameObject arrowArea;

    public float speedX = 0;
    public float speedY = 0;
    public Vector2 startPos;
    public bool isMove = true;
    private GameMain gameMain;
    public bool count = true;

	private Rigidbody rb; 

	Collider Waku_ObjectCollider; 

    public  bool position = true;

    public Vector3 mouseposition;

    private PhotonView photonview;
    private int thisViewId;


    // Use this for initialization
    private void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }


    void Start()
    {
        thisViewId = photonview.viewID;

        if (mine)
        {
            arrowArea = GameObject.Find("arrowArea");
            arrowArea.GetComponent<MouseController>().ResetData();
            arrowArea.GetComponent<MouseController>().SetParts(this.gameObject.GetComponent<MoveScript>());
            Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider>();
            gameMain = GameObject.Find("GameController").GetComponent<GameMain>();
            rb = gameObject.GetComponent<Rigidbody>();
        }
    }


    // Update is called once per frame
    void Update()
    {
		
       
    }


    public void Flip(Vector3 force)
    {
         if (mine)
        {
            // 瞬間的に力を加えてはじく
            this.rb.AddForce(force, ForceMode.Impulse);
        }
       
    }
		

    public void OnTriggerEnter(Collider c)
    {
       
            if (c.gameObject.tag == "Waku")
            {
                gameMain.FaceinAdd(this.gameObject);
                gameMain.GetComponent<PhotonView>().RPC("SetActivechange", PhotonTargets.All,thisViewId);//RPC使う必要あり

               if (count)
                {
                    gameMain.management = true;
                    count = false;
                    StartCoroutine(Stop());
                }

            }
    }
	
    	
    IEnumerator Stop()
	{
        yield return new WaitForSeconds (1.0f);
        int n = gameMain.nowNum;
        Debug.LogWarning("N="+n);
        object[] t = new object[]
       {
            n,PhotonNetwork.AllocateViewID()

       };
       
        isMove = false;
        gameMain.GetComponent<PhotonView>().RPC("StartGenerate", PhotonTargets.All, t);
        this.gameObject.tag = "Parts";
		this.gameObject.GetComponent<MoveScript> ().enabled = false;
		yield break;
        
    }


	void OnTriggerExit(Collider c)
	{
        if (photonview.isMine == true)
        {


            if (c.tag == "Waku")
            {
                Waku_ObjectCollider.isTrigger = false;
            }
        }
	}
    public bool Mine
    {

        set { mine = value; }
    }

}
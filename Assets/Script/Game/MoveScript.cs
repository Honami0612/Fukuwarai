using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class MoveScript : MonoBehaviour
{

    private bool mine = false;

    [SerializeField]
    int playerID = 1;
    [SerializeField]
    string playerID_string = "0";

    private GameObject arrowArea;

    private float speedX = 0;
    private float speedY = 0;
    private Vector2 startPos;
    
    private GameMain gameMain;
    private MouseController mouseController;
    bool count = true;
    bool isMove = true;

    private Rigidbody rb; 

	Collider Waku_ObjectCollider; 

    bool position = true;

    public Vector3 mouseposition;

    private PhotonView photonview;
    private int thisViewId;


    // Use this for initialization
    private void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }


    private void Start()
    {
        thisViewId = photonview.viewID;

        if (mine)
        {
            arrowArea = GameObject.Find("arrowArea");
            //arrowArea.GetComponent<MouseController>().ResetData();
            //arrowArea.GetComponent<MouseController>().SetParts(this.gameObject.GetComponent<MoveScript>());
            Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider>();
            gameMain = GameObject.Find("GameController").GetComponent<GameMain>();
            rb = gameObject.GetComponent<Rigidbody>();
        }
    }


    public void Flip(Vector3 force)//MouseCountroller.MouseUp
    {
         if (mine) this.rb.velocity = force;
    }
		

    public void OnTriggerEnter(Collider c)
    {
       
            if (c.gameObject.tag == "Waku")
            {
                gameMain.FaceinAdd(this.gameObject);
                gameMain.GetComponent<PhotonView>().RPC("SetActivechange", PhotonTargets.All,thisViewId);//RPC使う必要あり
                gameMain.GetComponent<PhotonView>().RPC("ID", PhotonTargets.MasterClient);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class MoveScript : MonoBehaviour
{

    private PhotonView photonview;

    bool mine = false;
    bool count = true;
    bool isMove = true;

    [SerializeField]
    int playerID = 1;
    [SerializeField]
    string playerIdString = "0";
    private int thisViewId;

    private GameObject arrowArea;

    private Rigidbody rb; 
	Collider Waku_ObjectCollider; 

    private GameMain gameMain;
    private MouseController mouseController;


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
            Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider>();
            gameMain = GameObject.Find("GameController").GetComponent<GameMain>();
            mouseController = GameObject.Find("arrowArea").GetComponent<MouseController>();
            rb = gameObject.GetComponent<Rigidbody>();
        }
        
    }


    private void Update()
    {
        if (mine)
        {
            Vector2 nowPart_velocity = this.rb.velocity;
            if ((gameMain.SetnowPartTransform.position.x < mouseController.SetLeftBottom.x) && (nowPart_velocity.x < 0))
                nowPart_velocity.x *= -1;
            if ((gameMain.SetnowPartTransform.position.x > mouseController.SetRightTop.x) && (nowPart_velocity.x >0))
                nowPart_velocity.x *= -1;
            if ((gameMain.SetnowPartTransform.position.y < mouseController.SetLeftBottom.y) && (nowPart_velocity.y < 0))
                nowPart_velocity.y *= -1;
            if ((gameMain.SetnowPartTransform.position.y > mouseController.SetRightTop.y) && (nowPart_velocity.y > 0))
                nowPart_velocity.y *= -1;

            this.rb.velocity = nowPart_velocity;

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
            if (c.tag == "Waku") Waku_ObjectCollider.isTrigger = false;
        }
	}


    public bool Mine
    {
        set { mine = value; }
    }

    
}

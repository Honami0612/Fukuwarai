using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    private GameObject arrowArea;

    public float speedX = 0;
    public float speedY = 0;
    public Vector2 startPos;
    public bool isMove = true;
    private GameController gameController;
    public bool count = true;

    private int a = 0;

	private Rigidbody rb; 

	Collider Waku_ObjectCollider; 

    public  bool position = true;

    public Vector3 mouseposition;

	//public float maxVelocity = 0.001f; //最大速度
	//private float maxSqrVelocity; //最大速度の2乗

    // Use this for initialization
    void Start()
    {
        arrowArea = GameObject.Find("arrowArea");
        arrowArea.GetComponent<MouseController>().ResetData();
        arrowArea.GetComponent<MouseController>().SetParts(this.gameObject.GetComponent<MoveScript>());
        Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider> ();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
		rb = gameObject.GetComponent<Rigidbody> ();

		//maxSqrVelocity = maxVelocity * maxVelocity; //最大速度の2乗を求めておく
	
 //       startPos = new Vector3(0, 0, 0);
   //     speedX = 0;
     //   speedY = 0;
       // count = true;


    }

    // Update is called once per frame
    void Update()
    {
		/*if (rb.velocity.sqrMagnitude > maxSqrVelocity)
		{
			Debug.Log ("speed");
			rb.velocity = rb.velocity.normalized * maxVelocity; //物理オブジェクトの速度をmaxVelocityで指定した最大速度にする
		}*/

        if (position == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("1");
                mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (mouseposition.x > 6.5f){

                    this.gameObject.transform.localPosition = new Vector3(mouseposition.x, mouseposition.y, 0);
                    position = false;

                }
                if (mouseposition.x < -6.5f)
                {
                    this.gameObject.transform.localPosition = new Vector3(mouseposition.x, mouseposition.y, 0);
                    position = false;
                }
                if (mouseposition.y > 7.5f)
                {
                    this.gameObject.transform.localPosition = new Vector3(mouseposition.x, mouseposition.y, 0);
                    position = false;
                }
                if (mouseposition.y < -7.5f)
                {
                    this.gameObject.transform.localPosition = new Vector3(mouseposition.x, mouseposition.y, 0);
                    position = false;
                }
            }
        }



   //     if (isMove)
   //     {
   //         if (Input.GetMouseButtonDown(0))
   //         {
   //             this.startPos = Input.mousePosition;
   //         }
   //         else if (Input.GetMouseButtonUp(0))
   //         { 
   //             Vector2 endPos = Input.mousePosition;

   //             float swipeLengthX = endPos.x - this.startPos.x;
   //             float swipeLengthY = endPos.y - this.startPos.y;
   //             this.speedX = swipeLengthX / 500.0f;
   //             this.speedY = swipeLengthY / 500.0f;
			//	/*if (this.speedX >1) {
			//		this.speedX = 1;

			//	} else if (this.speedY >1) {
			//		this.speedY = 1;
			//	}*/
			//	gameController.management = true;
			
   //         }


   //         transform.Translate(this.speedX, this.speedY, 0);
   //         this.speedX *= 0.98f;
   //         this.speedY *= 0.98f;

			////Debug.Log (this.speedX);
			////Debug.Log (this.speedY);



        //}
    }

    public void Flip(Vector3 force)
    {
        // 瞬間的に力を加えてはじく
        this.rb.AddForce(force, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Waku")
        {
			Debug.Log ("check");
            if (count)
            {
                gameController.management = true;
                count = false;
                StartCoroutine(Stop());
              
               
            }

        }
    }

    IEnumerator Stop()
	{
		yield return new WaitForSeconds (1.0f);
		isMove = false;
		gameController.Generate ();
        this.gameObject.tag = "Parts";
		this.gameObject.GetComponent<MoveScript> ().enabled = false;
        //yield break;
        
    }

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Waku")
		{
			Waku_ObjectCollider.isTrigger = false;
		}
	}
}
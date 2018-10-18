using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{


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



    // Use this for initialization
    void Start()
    {
		Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider> ();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
		rb = gameObject.GetComponent<Rigidbody> ();
	
 //       startPos = new Vector3(0, 0, 0);
   //     speedX = 0;
     //   speedY = 0;
       // count = true;


    }

    // Update is called once per frame
    void Update()
    {

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



        if (isMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            { 
                Vector2 endPos = Input.mousePosition;

                float swipeLengthX = endPos.x - this.startPos.x;
                float swipeLengthY = endPos.y - this.startPos.y;
                this.speedX = swipeLengthX / 500.0f;
                this.speedY = swipeLengthY / 500.0f;

				gameController.management = true;
			
            }


            transform.Translate(this.speedX, this.speedY, 0);
            this.speedX *= 0.98f;
            this.speedY *= 0.98f;


        }
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Waku")
        {
			Debug.Log ("check");
            if (count)
            {
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

        
    }

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Waku")
		{
			Waku_ObjectCollider.isTrigger = false;
		}
	}
}
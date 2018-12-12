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

    void Start()
    {
        arrowArea = GameObject.Find("arrowArea");
        arrowArea.GetComponent<MouseController>().ResetData();
        arrowArea.GetComponent<MouseController>().SetParts(this.gameObject.GetComponent<MoveScript>());
        Waku_ObjectCollider = this.gameObject.GetComponent<BoxCollider> ();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
		rb = gameObject.GetComponent<Rigidbody> ();

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
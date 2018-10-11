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

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
 //       startPos = new Vector3(0, 0, 0);
   //     speedX = 0;
     //   speedY = 0;
       // count = true;

    }

    // Update is called once per frame
    void Update()
    {

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
            Debug.Log("check");
            if (count)
            {
                count = false;
                StartCoroutine(Stop());
              
               
            }
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2.0f);
        isMove = false;
        gameController.Generate();
        this.gameObject.GetComponent<MoveScript>().enabled = false;
        
    }

}


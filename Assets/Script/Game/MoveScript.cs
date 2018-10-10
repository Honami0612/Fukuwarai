using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{


    public float speedX = 0;
    public float speedY = 0;
    Vector2 startPos;
    bool isMove = true;
    private GameObject go;
    bool count = true;

    // Use this for initialization
    void Start()
    {
        go = GameObject.Find("GameObject");
        GameObject parts = GameObject.Find("parts");
       

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
                Invoke("stop", 2.0f);
                count = false;
            }
        }
    }
    public void stop()
    {
        go.GetComponent<P_Generator>().pGenerate();
        isMove = false;
    }

}


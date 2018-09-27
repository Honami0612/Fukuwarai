using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


     float speedX = 0;
    float speedY = 0;
    Vector2 startPos;
    GameObject parts1;
    GameObject parts2;
    GameObject parts3;
    GameObject parts4;
    GameObject parts5;
    GameObject parts6;

    // Use this for initialization
    void Start () {

        parts1 = GameObject.Find("parts1");
        parts2 = GameObject.Find("parts2");
        parts3 = GameObject.Find("parts3");
        parts4 = GameObject.Find("parts4");
        parts5 = GameObject.Find("parts5");
        parts6 = GameObject.Find("parts6");

    }
	
	// Update is called once per frame
	 void Update() {

        
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;

            float swipeLengthX = endPos.x - startPos.x;
            float swipeLengthY = endPos.y - startPos.y;
            this.speedX = swipeLengthX / 250.0f;
            this.speedY = swipeLengthY / 250.0f;
        }

        
        transform.Translate(this.speedX, this.speedY, 0);
        this.speedX *= 0.98f;
        this.speedY *= 0.98f;

	}

}

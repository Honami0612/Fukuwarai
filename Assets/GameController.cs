using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


     float speedX = 0;
    float speedY = 0;
    Vector2 startPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;

            float swipeLengthX = endPos.x - startPos.x;
            float swipeLengthY = endPos.y - startPos.y;
            this.speedX = swipeLengthX / 500.0f;
            this.speedY = swipeLengthY / 500.0f;
        }

        
        transform.Translate(this.speedX, this.speedY, 0);
        this.speedX *= 0.98f;
        this.speedY *= 0.98f;

	}
}

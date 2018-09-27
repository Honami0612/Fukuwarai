using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public bool trigger;
    private Vector2 startPos;
    private Vector3 inputPos;

    private Collider collider;
     
    [SerializeField]
    float speedX = 0;
    [SerializeField]
    float speedY = 0;

	// Use this for initialization
	void Start () {
        trigger = false;
        collider = this.gameObject.GetComponent<Collider>();
	}

    // Update is called once per frame
    void Update()
    {
        if (trigger == true)
        {

            if (Input.GetMouseButtonUp(1))
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

        if (Input.GetMouseButtonDown(1))
        {
            Ray tapPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (collider.Raycast(tapPosition, out hit, 10F))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("aaaa");
                if (hit.collider.gameObject.name == this.gameObject.name)
                {
                    OnClickObject();
                }
            }           
        }

    }

        public void OnClickObject()
    {
        trigger = !trigger;
    }

}

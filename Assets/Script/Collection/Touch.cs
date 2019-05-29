using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	private GameObject collection;
	private bool Click = false;

	// Use this for initialization
	void Start () {
		collection = GameObject.Find ("Collection(Clone)");
		Debug.Log (collection);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			OnMouseDown ();
			if (Click) {
				Debug.Log ("false"+Click);
				Click = false;
			} else {
				Click = true;
				Debug.Log ("true"+Click);
			}
		}
	}

	void OnMouseDown(){
		//どのオブジェクトがタッチされたか探す
		Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D aCollider2d = Physics2D.OverlapPoint (aTapPoint);
		if (aCollider2d) {
			GameObject obj = aCollider2d.transform.gameObject;
			/*if (obj.name=collection.name) {
				if (Click) {
					Debug.Log ("white");
				} else {
					Debug.Log ("blue");
				}
			}*/

		}

	}
}

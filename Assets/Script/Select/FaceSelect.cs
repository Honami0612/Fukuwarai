using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceSelect : MonoBehaviour {

	[SerializeField]
	GameObject[] face;

	public static int selectnumber=1;

	// Use this for initialization
	void Start () {
		for(int i=0;i<face.Length;i++){
			Instantiate (face [i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int Select(){
		return selectnumber;
	}



/*
	public GameObject getClickObject(){
		GameObject result = null;

		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				result = hit.collider.gameObject;
			}
		}
		Debug.Log ("Object" + result);
		return result;
		}
*/

		//SceneManager.LoadScene ("GameScene");



}

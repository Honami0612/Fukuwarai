using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenestart : MonoBehaviour {


 /*  void Update(){

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Scene");
            SceneManager.LoadScene("GameScene");
        }
	}*/

	public void OnClick(int number){
		switch (number) {
		case 0:
			SceneManager.LoadScene ("GameScene");
			Debug.Log ("0");
			break;
		case 2:
			SceneManager.LoadScene ("Collection");
			Debug.Log ("1");
			break;

		}
	}
}

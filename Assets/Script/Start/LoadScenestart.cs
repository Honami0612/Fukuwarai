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
			break;
		case 1:
			SceneManager.LoadScene ("Collection");
			break;
		}
	}
}

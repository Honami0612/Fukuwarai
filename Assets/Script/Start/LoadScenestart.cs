using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenestart : MonoBehaviour {



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

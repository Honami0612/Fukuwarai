using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceSelect : MonoBehaviour {


	private static int selectnumber = 0;

	public void ClickKao(int number){
		SelectNumber = number;
		Debug.Log ("number"+number);
		SceneManager.LoadScene ("GameScene");
	}

	public static int SelectNumber {
		get{ return selectnumber; }
		set { selectnumber = value; }
	}

}

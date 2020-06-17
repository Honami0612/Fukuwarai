using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//このスクリプト自体いらないw

public class PlayerScript : NetworkBehaviour {

	private GameMain gameController;

	void Start ()
    {
		gameController = GameObject.Find ("GameController").GetComponent<GameMain> ();
	}
	
}

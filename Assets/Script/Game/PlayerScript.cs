using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {

	private GameMain gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameMain> ();
		CmdGenerate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdGenerate(){
		gameController.Generate ();
	}
}

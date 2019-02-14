using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	//移動速度
	[SerializeField]
	float m_WalkSpeed=4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//ローカルプレイヤーの場合のみ
		//移動処理を実施する
		if (isLocalPlayer) {
			//移動量を求める
			Vector3 motion=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*m_WalkSpeed*Time.deltaTime;
			if (motion.magnitude > 0f) {
				//Commandにより、移動の依頼をサーバーに発行
				CmdMove(motion);
			}
		}
	}

	//移動処理。これはサーバーで実行される
	[Command]
	void CmdMove(Vector3 motion){
		print ("CmdMove,netId=" + netId + ",motion=" + motion);//仮。とりあえずログ確認

	}
}

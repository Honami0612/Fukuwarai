using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveRoom : MonoBehaviour {
    //これはcanvasとかにつける？？
    //退出ボタンを押されて誰かが退出したらこれが呼ばれるはず


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPhotonPlayerDisconnect(PhotonPlayer otherplayer)//誰かがルームを退出したときに呼ばれる
    {

    }
}

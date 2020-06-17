﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameFinish : MonoBehaviour {

    PhotonView photonView;
    [SerializeField]
    LobbyManager lobbyManager;
    [SerializeField]
    GameMain gameMain;

    public Button finishstart;
    public Button leaveRoom;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        //Clientのボタンコンポーネント削除
        if (PhotonNetwork.isNonMasterClientInRoom)
        {
            Destroy(finishstart.GetComponent<Button>());
            Destroy(leaveRoom.GetComponent<Button>());  
        }

        photonView.RPC("ResetCount", PhotonTargets.All);
        Debug.Log("Reset:" + gameMain.nowCount);
    }

    public void Onclick(int number)
    {
        switch (number)
        {
            case 0://スタートシーンへ移動
                   photonView.RPC("GoStart", PhotonTargets.All);
            　　　　break;
            
            case 2://ルーム退出
                photonView.RPC("LeaveRoom", PhotonTargets.All);
                //photonView.RPC("GoLobby", PhotonTargets.All);


                break;
        }
    }

    [PunRPC]
    public void GoStart()
    {
        SceneManager.LoadScene("Start");
    }


    //[PunRPC]
    //void GoLobby()
    //{
    //    SceneManager.LoadScene("Lobby");
    //}

    [PunRPC]
    void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;//Unityエディタ用
        Application.Quit();//build用
    }

    [PunRPC]
    public void LeaveRoom()
    {
        photonView.RPC("Quit", PhotonTargets.All);
        //photonView.RPC("GoLobby", PhotonTargets.All);
        if (PhotonNetwork.LeaveRoom())
        {
            Debug.Log("ルームを退出しました");
        }
        else
        {
            Debug.Log("ルーム退出できませんでした");
        }

    }

    [PunRPC]
    void ResetCount()
    {
        gameMain.nowCount = 0;
    }

    void CloseConnection()
    {
        Debug.Log("Master");
    }

}


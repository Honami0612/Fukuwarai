using System.Collections;
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

    [SerializeField]
    Button finishstart;
    [SerializeField]
    Button leaveRoom;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

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
                   break;
        }
    }


    [PunRPC]
    public void GoStart()
    {
        SceneManager.LoadScene("Start");
    }


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
    }


    [PunRPC]
    void ResetCount()
    {
        gameMain.nowCount = 0;
    }

}


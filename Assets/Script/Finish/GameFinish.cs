using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameFinish : MonoBehaviour {

    PhotonView photonView;
    [SerializeField]
    LobbyManager lobbyManager;

    public Button finishstart;
    public Button finishcollection;
    public Button leaveRoom;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.isNonMasterClientInRoom)
        {
           
            Destroy(finishstart.GetComponent<Button>());
            Destroy(finishcollection.GetComponent<Button>());
            Destroy(leaveRoom.GetComponent<Button>());
           
        }
    }

    public void Onclick(int number)
    {
        switch (number)
        {
            case 0://スタートシーンへ移動
                   photonView.RPC("GoStart", PhotonTargets.All);
            　　　　break;
            case 1://コレクションシーンへ移動
                   photonView.RPC("GoCollection", PhotonTargets.All);
            　　　　break;
            case 2://ルーム退出
                photonView.RPC("GoLobby", PhotonTargets.All);
                photonView.RPC("LeaveRoom",PhotonTargets.All);
                   
                   break;
        }
    }

    [PunRPC]
    public void GoStart()
    {
        SceneManager.LoadScene("Start");
    }

    [PunRPC]
    void GoCollection()
    {
        SceneManager.LoadScene("Select");
    }
    [PunRPC]
    void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    [PunRPC]
    public void LeaveRoom()
    {
        if (PhotonNetwork.LeaveRoom())
        {
            Debug.Log("ルームを退出しました");

        }
        else
        {
            Debug.Log("ルーム退出できませんでした");
        }

    }

}


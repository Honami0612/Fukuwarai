using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Canvasにつける
public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    Text message;

    // roomNameを書くとこ
    [SerializeField]
    GameObject roomNameArea;


    //今あるroom一覧
    [SerializeField]
    GameObject roomListArea;
    public List<GameObject> roomList;
    [SerializeField]
    GameObject roomPrefub;

    
    public Button GameStart;

    //入る　or 作る　ルーム名
    private string roomName;


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("バージョン番号");
        UpDateRoom();
        GameStart.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    public void UpDateRoom()
    {
        int i = 0;
        foreach(GameObject obj in roomList)
        {
            Destroy(roomList[i].gameObject);
            i++;
        }

        roomList.Clear();

        i = 0;
        RoomInfo[] roomInfos = PhotonNetwork.GetRoomList();
        if (roomInfos.Length == 0)
        {
            message.text = "ルームがありません";
        }
        else
        {
            foreach (RoomInfo room in roomInfos)
            {
                roomList.Add(Instantiate(roomPrefub,transform));
                roomList[i].transform.parent = roomListArea.transform;
                //roomList[i].transform.SetParent(roomListArea.transform);
                roomList[i].GetComponentsInChildren<Text>()[0].text = room.Name;
                int j = i;
                roomList[i].GetComponent<Button>().onClick.AddListener(() => RoomJoin(j));
                i++;

            }
        }
        message.text = "ルームを更新しました";
    }

    // ルーム作成
    public void RoomCreate()
    {
        //roomNameをTextからとる
        roomName = GameObject.Find("InputRoomName").GetComponent<InputField>().text;
        Debug.Log(roomName);
        //roomを作成できた時の処理
        if (PhotonNetwork.CreateRoom(roomName))
        {
            message.text = "ルームの作成に成功しました";
            Debug.Log("ルーム作成に成功しました");
        }
        else
        {
            message.text = "ルームの作成に失敗しました";
            Debug.Log("ルーム作成に失敗しました");
        }
    }

    // ルームに入室する
    public void RoomJoin(int number)
    {
        roomName = roomList[number].GetComponentsInChildren<Text>()[0].text;
        if (PhotonNetwork.JoinRoom(roomName))
        {
            message.text = "ルームに入室しました";

        }
        else
        {
            message.text = "ルームに入室できませんでした";
        }
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinRoom");
        PhotonPlayer photonPlayer = PhotonNetwork.masterClient;
        Debug.Log("Player" + photonPlayer);
        if (photonPlayer != null)
        {
            GameStart.gameObject.SetActive(true);
        }
    }

    public void OnPhotonJoinRoomFailed()
    {
        message.text = "ルームに入れませんでした";
        Debug.Log("ルームに入れませんでした");
    }

    //ルームから退出する時使って
    public void LeaveRoom()
    {
        if (PhotonNetwork.LeaveRoom())
        {
            message.text = "ルームから退出しました";
        }
        else
        {
            message.text = "ルームから退出できませんでした";
        }
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }

}
//生成するプレハブ、ボタンとテキスト
//GetRoomList(PhotonNetwork.cs)をUpDateでまわしつづける
//1回目に呼んだルーム数と2回目に呼んだルーム数が違った場合ルームの数をふやす（常に更新）

//start()でInstanseなどを一度まわして、更新ボタンを押した場合とーやさんの写真（元のサイト）のをまわす
//oldと現在のを比較して違った場合のみ（元のサイト）をまわす

//PhotonPlayer PhotonNetwork.masterClient //マスターかマスターでないかを判断できる。マスターがスタートボタンを押したらゲームスタート
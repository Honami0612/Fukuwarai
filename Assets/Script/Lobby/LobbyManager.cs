using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    Text onlyClient;

    //今あるroom一覧
    [SerializeField]
    GameObject roomListArea;
    [SerializeField]
    List<GameObject> roomList;
    [SerializeField]
    GameObject roomPrefub;

    [SerializeField]
    Button gameStart;

    //入る　or 作る　ルーム名
    private string roomName;

    private bool photonPlayer;
    private RoomInfo[] roomInfos;



    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("バージョン番号");

        roomListArea.SetActive(true);
        gameStart.gameObject.SetActive(false);
        onlyClient.gameObject.SetActive(false);

        UpDateRoom();
    }

   

    void OnReceivedRoomListUpdate()
    {
        UpDateRoom();
    }


    public void UpDateRoom()
    {
          roomInfos = PhotonNetwork.GetRoomList();

         int i = 0;
        foreach(GameObject obj in roomList)
        {
            Destroy(roomList[i].gameObject);
            i++;
        }

        roomList.Clear();

        i = 0;
        if (roomInfos.Length == 0)
        {
            //ルームなし
        }
        else
        {
            foreach (RoomInfo room in roomInfos)
            {
                roomList.Add(Instantiate(roomPrefub,transform));
                roomList[i].transform.parent = roomListArea.transform;
                roomList[i].GetComponentsInChildren<Text>()[0].text = room.Name;
                int j = i;
                roomList[i].GetComponent<Button>().onClick.AddListener(() => RoomJoin(j));
                i++;
            }
        }
    }

    // ルーム作成
    public void RoomCreate()
    {
        roomName = GameObject.Find("InputRoomName").GetComponent<InputField>().text;
        if (PhotonNetwork.CreateRoom(roomName)) roomListArea.SetActive(false);//ルーム作成に成功
       
    }


    // ルームに入室する
    public void RoomJoin(int number)
    {
        roomName = roomList[number].GetComponentsInChildren<Text>()[0].text;
        if (PhotonNetwork.JoinRoom(roomName)) roomListArea.SetActive(false);//ルームに入室
    }


    //ルーム入室に成功すると自動で呼び出される
    public void OnJoinedRoom()
    {
        photonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (photonPlayer !=true)
        {
            gameStart.gameObject.SetActive(true);
        }
        else
        {
            onlyClient.gameObject.SetActive(true);
        }
    }


    //ルームに入れなかった際
    public void OnPhotonJoinRoomFailed()
    {
        Debug.Log("ルームに入れませんでした");
    }


    //ルーム退出
    public void LeaveRoom()
    {
        if (PhotonNetwork.LeaveRoom())
        {
            gameStart.gameObject.SetActive(false);
            roomListArea.SetActive(true);
            onlyClient.gameObject.SetActive(false); 
        }
    }


    //ルーム新規作成
    public void SetActive()
    {
        roomListArea.gameObject.SetActive(false);
    }

   
}

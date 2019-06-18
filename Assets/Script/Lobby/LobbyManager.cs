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

    //顔の数分 public in
    public int[] appleparts;
    public int[] ojiparts;
    public int[] moonparts;
    public int[] rabittoparts;

    //public no
    private int[][] parts;



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


    [SerializeField]
    int maxplayer; //最大人数

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("バージョン番号");
        UpDateRoom();
        roomListArea.SetActive(true);
        GameStart.gameObject.SetActive(false);

        parts[0] = appleparts;
        parts[1] = ojiparts;
    }

   void OnReceivedRoomListUpdate()
    {
        UpDateRoom();
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
            roomListArea.SetActive(false);
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
            roomListArea.SetActive(false);

        }
        else
        {
            message.text = "ルームに入室できませんでした";
        }
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinRoom");
       // PhotonPlayer photonPlayer = PhotonNetwork.isNonMasterClientInRoom();
        bool photonPlayer= PhotonNetwork.isNonMasterClientInRoom;
        Debug.Log("Player" + photonPlayer);
        message.text = "test" + photonPlayer;
        if (photonPlayer !=true)//MasterClientでないときtrueが返ってくる
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
            GameStart.gameObject.SetActive(false);
            message.text = "ルームから退出しました";
            roomListArea.SetActive(true);
        }
        else
        {
            message.text = "ルームから退出できませんでした";
        }
    }

    public void SetActive()
    {
        roomListArea.gameObject.SetActive(false);
    }


}

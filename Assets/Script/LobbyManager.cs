using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManager;

// Canvasにつける
public class LobbyManager : MonoBehaviour {
  [SerializeField]
  Text message;

  // roomNameを書くとこ
  [SerializeField]
  GameObject roomNameArea;

  //今あるroom一覧
  [SerializeField]
  GameObject roomListArea;
  private List<GameObject> roomList;
  [SerializeField]
  GameObject roomPrefub;

  //入る　or 作る　ルーム名
  private string roomName;

  private void Start(){
    PhotonNetwork.ConnectUsingSettings("バージョン番号");
  }

  // ルーム作成
  public void RoomCreate(){
    //roomNameをTextからとる
    roomName = roomNameArea.GetComponent<Text>().text;
    //roomを作成できた時の処理
    if(PhotonNetwork.CreateRoom(roomName)){
      roomList.Add(Instantiate(roomPrefub));
      // 作成したルームの設定
      int number = roomList.Count - 1
      roomList[number].transform = roomListArea.parent;
      roomList[number].GetComponent<Button>().onClick.AddListener( () => JoinRoom(number) );
      message.text = "ルームの作成に成功しました";
    }else{
      message.text = "ルームの作成に失敗しました";
    }
  }

  // ルームに入室する
  public void JoinRoom(int number){
    roomName = roomList[number].GetComponent<Text>().text;
    if(PhotonNetwork.JoinRoom(roomName)){
        message.text = "ルームに入室しました";
    }else{
      message.text = "ルームに入室できませんでした";
    }
  }

  // ルームから退出する時使って
  // public void LeaveRoom(){
  //   if (PhotonNetwork.LeaveRoom()){
  //
  //   }else{
  //
  //   }
  // }
  
  public void ToGameScene(){
    SceneManager.LoadScene("Game");
  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewRoom : MonoBehaviour {

    [SerializeField]
    LobbyManager lobbyManager;

    public GameObject InputField;
    public Button DecisionButton;

    [SerializeField]
    Text participationPeople;

    [SerializeField]
    Text onlyMaster;


	// Use this for initialization
	void Start ()
    {
        InputField.SetActive(false);
        DecisionButton.gameObject.SetActive(false);
        onlyMaster.gameObject.SetActive(false);
       
	}


    private void Update()
    {
        participationPeople.text = "現在の参加人数:" + PhotonNetwork.playerList.Length.ToString();
    }



    public void OnClick(int number)
    {
        switch (number)
        {
            case 0://CreateNewRoomButton
                lobbyManager.SetActive();
                InputField.SetActive(true);
                DecisionButton.gameObject.SetActive(true);

                break;


            case 1://DecisionButton
                lobbyManager.RoomCreate();
                InputField.SetActive(false);
                DecisionButton.gameObject.SetActive(false);
                onlyMaster.gameObject.SetActive(true);

                break;
            case 2://GameStartButtom(masteronly)
                Debug.Log("masterbuttonclick");
                PhotonView t =GetComponent<PhotonView>();
                t.RPC("GoGame",PhotonTargets.All);
          
                break;
            case 3://UpdateButtom
                //lobbyManager.UpDateRoom();

                break;
            case 4:
                lobbyManager.LeaveRoom();
                onlyMaster.gameObject.SetActive(false);
                break;
        }

    }


    [PunRPC]
    public void GoGame()
    {
        
       // GameObject b= Instantiate(a);
        //me.text = a.GetComponent<PhotonView>().isMine.ToString();
        if (PhotonNetwork.inRoom)
         {
             Debug.Log("inRoom");
             PhotonNetwork.room.IsOpen = false;
             PhotonNetwork.room.IsVisible = false;
         }
         SceneManager.LoadScene("Start");

    }
}

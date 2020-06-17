using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewRoom : MonoBehaviour {

    [SerializeField]
    LobbyManager lobbyManager;

    [SerializeField]
    GameObject InputField;
    [SerializeField]
    Button DecisionButton;
    [SerializeField]
    Button ExitButton;

    [SerializeField]
    Text participationPeople;

    [SerializeField]
    Text onlyMaster;


	private void Start ()
    {
        InputField.SetActive(false);
        DecisionButton.gameObject.SetActive(false);
        onlyMaster.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
       
	}


    private void Update()
    {
        participationPeople.text = "The number of participants:" + PhotonNetwork.playerList.Length.ToString();

        if (PhotonNetwork.inRoom)
        {
            ExitButton.gameObject.SetActive(true);
        }
        else
        {
            ExitButton.gameObject.SetActive(false);
        }
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
        if (PhotonNetwork.inRoom)
         {
             PhotonNetwork.room.IsOpen = false;
             PhotonNetwork.room.IsVisible = false;
         }

         SceneManager.LoadScene("Start");
    }
}

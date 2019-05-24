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



	// Use this for initialization
	void Start () {
		InputField.SetActive(false);
        DecisionButton.gameObject.SetActive(false);
       
	}
	
	// Update is called once per frame
	void Update ()
    {
   
    }


   
    public void OnClick(int number)
    {
        switch (number)
        {
            case 0://CreateNewRoomButton
            //InputField.GetComponent<Text>().text = "";
            InputField.SetActive(true);
                DecisionButton.gameObject.SetActive(true);
                break;


            case 1://DecisionButton
                lobbyManager.RoomCreate();
                InputField.SetActive(false);
                DecisionButton.gameObject.SetActive(false);

                break;
            case 2://GameStartButtom(masteronly)
                Debug.Log("masterbuttonclick");
                //SceneManager.LoadScene("Start");
                break;
            case 3://UpdateButtom
                lobbyManager.UpDateRoom();

                break;
            case 4:
                lobbyManager.LeaveRoom();
                break;
        }

    }
}

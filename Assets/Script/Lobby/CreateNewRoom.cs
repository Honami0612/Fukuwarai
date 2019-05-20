using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewRoom : MonoBehaviour {

    private LobbyManager lobbyManager;

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
            case 0://InputField.GetComponent<Text>().text = "";
            InputField.SetActive(true);
                DecisionButton.gameObject.SetActive(true);
                break;


            case 1:InputField.SetActive(false);
                DecisionButton.gameObject.SetActive(false);
                lobbyManager.RoomCreate();
                break;
            case 2:
               // lobbyManager.JoinRoom();
                break;
        }

    }
}

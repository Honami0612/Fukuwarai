using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FaceSelect : MonoBehaviour {

    [SerializeField]
    GameObject ojisan;
    [SerializeField]
    GameObject man;
    [SerializeField]
    GameObject apple;
    [SerializeField]
    GameObject moon;
    [SerializeField]
    GameObject rabbit;

    private void Start()
    {
        bool phtotonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (phtotonPlayer == true)
        {
            Destroy(ojisan.GetComponent<EventTrigger>());
            Destroy(man.GetComponent<EventTrigger>());
            Destroy(apple.GetComponent<EventTrigger>());
            Destroy(moon.GetComponent<EventTrigger>());
            Destroy(rabbit.GetComponent<EventTrigger>());
        }
    }
    private static int selectnumber = 0;

	public void ClickKao(int number){

        PhotonView t = GetComponent<PhotonView>();
        t.RPC("GoGameScene", PhotonTargets.All,number);

		
	}
    
    [PunRPC]
    public void GoGameScene(int number)
    {
        SelectNumber = number;
        Debug.Log("number" + number);
        SceneManager.LoadScene("GameScene");
    }


    public static int SelectNumber {
		get{ return selectnumber; }
		set { selectnumber = value; }
	}

}

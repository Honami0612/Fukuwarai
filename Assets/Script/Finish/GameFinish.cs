using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameFinish : MonoBehaviour {

    public Button selectstart;
    public Button finishstart;
    public Button finishcollection;

    private void Start()
    {
        bool phtotonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (phtotonPlayer == true)
        {
            selectstart.enabled = false;
        }
    }

    public void OnclickStart()
    { 

        PhotonView t = GetComponent<PhotonView>();
        t.RPC("GoStart", PhotonTargets.All);
    }

    [PunRPC]
    public void GoStart()
    {

        SceneManager.LoadScene("Start");
    }
}


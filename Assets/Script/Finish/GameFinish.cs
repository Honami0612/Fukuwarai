using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameFinish : MonoBehaviour {

    
    public void OnclickStart()
    {
        //SceneManager.LoadScene("Start");
        PhotonView t = GetComponent<PhotonView>();
        t.RPC("GoStart", PhotonTargets.All);
    }

    [PunRPC]
    public void GoStart()
    {
        bool photonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (photonPlayer != true)
        {
            SceneManager.LoadScene("Start");
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenestart : MonoBehaviour {


    public Button select;
    public Button collection;

    private void Start()
    {
        bool photonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (photonPlayer == true)
        {
           
            select.enabled = false;
            collection.enabled = false;
        }
       
    }

    public void OnClick(int number){
		switch (number) {
        case 0:
                //SceneManager.LoadScene ("Select");
                PhotonView t = GetComponent<PhotonView>();
                t.RPC("GoSelect", PhotonTargets.All);
                //Debug.Log ("0");
                
			break;
		case 1:
                //SceneManager.LoadScene ("Collection");
                PhotonView s = GetComponent<PhotonView>();
                s.RPC("GoCollection", PhotonTargets.All);
                Debug.Log ("1");
			break;

		}
	}

    [PunRPC]
    public void GoSelect()
    {
        SceneManager.LoadScene("Select");
    }

    [PunRPC]
    public void GoCollection()
    {
            SceneManager.LoadScene("Collection");

    }
}

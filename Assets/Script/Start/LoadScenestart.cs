using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenestart : MonoBehaviour {

    [SerializeField]
    Button select;
    [SerializeField]
    Button collection;

    private void Start()
    {
        bool photonPlayer = PhotonNetwork.isNonMasterClientInRoom;
        if (photonPlayer == true)
        {
            select.enabled = false;
            collection.enabled = false;
        } 
    }

    public void OnClick(int number)
    {
		switch (number)
        {
        case 0:
                SceneManager.LoadScene("Select");
                PhotonView t = GetComponent<PhotonView>();
                t.RPC("GoSelect", PhotonTargets.All);
			break;

		case 1: //準備中
                //SceneManager.LoadScene("Collection");
                PhotonView s = GetComponent<PhotonView>();
                s.RPC("GoCollection", PhotonTargets.All);
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

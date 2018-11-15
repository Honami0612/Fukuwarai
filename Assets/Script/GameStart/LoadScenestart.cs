using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenestart : MonoBehaviour {


   void Update(){

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Scene");
            SceneManager.LoadScene("GameScene");
        }
	}
}

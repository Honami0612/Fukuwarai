using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {


	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {

            GetComponent<AudioSource>().Play();

        }
		
	}
}

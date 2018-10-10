using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioClip bgmSound;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(bgmSound);
        }
		
	}
}

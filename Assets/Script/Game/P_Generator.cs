using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Generator : MonoBehaviour {

    public GameObject partsPrefab;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
    public void pGenerate()
    {
        Debug.Log("pGene");
        GameObject parts = Instantiate(partsPrefab) as GameObject;

    }
}

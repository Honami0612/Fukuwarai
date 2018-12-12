using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


public class collection : MonoBehaviour {

	public GameObject CollectionPrefab;
	private List<GameObject> Collection = new List<GameObject>();

	int i,n,m,num;

	public Transform canvasObject;

	// Use this for initialization
	void Start () {
		StreamReader sr = new StreamReader (Application.dataPath+"/Resources/ScreenShotnumber.csv");
		while (sr.Peek () > -1) {
			string line = sr.ReadLine ();
			num=int.Parse(line);
		}

		for (i = 0; i < num; i++) {
			Texture2D texture = Resources.Load ("Sprite/ScreenShot/savedata" + i)as Texture2D;

			Collection.Add (Instantiate (CollectionPrefab, new Vector2 (200.0f*i/*横*/, 500.0f/*縦*/), Quaternion.Euler (0, 0, 0))as GameObject);
			Collection [i].transform.SetParent (canvasObject);
			Collection [i].GetComponent<Image> ().sprite = Sprite.Create (texture, new Rect (0, 0, 512, 256), Vector2.zero);
		}


	}
	
}

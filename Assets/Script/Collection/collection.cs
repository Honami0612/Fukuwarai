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

			Collection.Add (Instantiate (CollectionPrefab)as GameObject);
			Collection [i].transform.SetParent (canvasObject);
			Collection [i].GetComponent<Image> ().sprite = Sprite.Create (texture, new Rect (0, 0, 589, 331), Vector2.zero);//200,250,500,300
		}


	}

	void Update(){

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject partsPrefab;

    [SerializeField]
    Sprite[] partsSprite;

	[SerializeField]
	List<GameObject> parts = new List<GameObject>();
    private int num;

	private bool a = false;

    private void Start()
    {
        num = 0;

        GameObject.Find("parts").GetComponent<SpriteRenderer>().sprite = partsSprite[num];
    }

	void Update(){
		if(a==true){
			a = false;
			Invoke ("PosStop",1.2f);
		}
	}

    public void Generate()
    {
        Debug.Log("確認");
        num++;
		parts.Add (Instantiate (partsPrefab) as GameObject);
		parts[num].name = partsPrefab.name;
		parts[num].GetComponent<SpriteRenderer>().sprite = partsSprite[num];


 //       mouth.transform.localPosition = new Vector3(0, 0, 0);	
    }

	void PosStop()
	{
		
//		GameObject[] Parts = GameObject.FindGameObjectsWithTag ("Parts");
		//GameObject[] Parts = GameObject.Find("parts");
		for(int j=0;j<parts.Count;j++)
		{
			Debug.Log ("PosStop");
			parts[j].GetComponent<Rigidbody>().isKinematic = true;
		}
		Triggerfalse ();
	}
		
	void Triggerfalse()
	{
		//GameObject[] Parts = GameObject.FindGameObjectsWithTag ("Parts");
		//GameObject[] Parts = GameObject.Find("parts");
		for(int i=0;i<parts.Count;i++){
			Debug.Log ("Triggerfalse");
			parts[i].GetComponent<Rigidbody> ().isKinematic = false;
		}
	}

	public bool A{
		get { return a;}
		set { a = value; }
	}
}

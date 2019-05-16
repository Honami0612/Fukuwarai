using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
//using UnityEngine.Networking;

public class GameMain : MonoBehaviour {

	//public GameObject a;

    [SerializeField]
    GameObject partsPrefab;

    [SerializeField]
    Sprite[] partsSprite;

    [SerializeField]
    Text timerText;

	//[SyncVar]
    private int num;

    float time = 10.0f;
	private bool posmanagement = false;

    [SerializeField]
    GameObject screenshotPrefab;
    GameObject screenshot;

	[SerializeField]
	GameObject[] kao;

	public GameObject[] partsLoad;
	[SerializeField]
	List<GameObject> parts = new List<GameObject>();

	string[] folder={"1ojisan","2man","3apple","4moon","5rabbit"};

    private void Start()
    {
        num = 0;
		int faceselectnumber = FaceSelect.SelectNumber - 1;
		Debug.Log (faceselectnumber);
		Instantiate (kao[faceselectnumber]);//顔生成

        if (GameObject.Find("ScreenShot(Clone)")==null)
        {
            screenshot = Instantiate(screenshotPrefab);
        }
        else
        {
            screenshot = GameObject.Find("ScreenShot(Clone)"); 
        }
       
        partsLoad = Resources.LoadAll <GameObject> ("Game/"+folder[faceselectnumber]); //呼び出し一括


		parts.Add(Instantiate(partsLoad[0])); //パーツ生成
		parts[0].SetActive(true);
		parts[0].transform.localPosition = new Vector3(0, -11.4f, 0);
		parts[0].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
		parts[0].transform.localRotation = Quaternion.Euler(0, 0, 0);

		this.time=10.0f;


    }

	void Update() { 

		//if (isLocalPlayer) {
			/*this.time -= Time.deltaTime;

			if (this.time < 0) {
				Generate ();
			} else {
				this.timerText.text = this.time.ToString ("F0");
			}*/
	//	} 

			if (posmanagement == true) {
				posmanagement = false;
				Invoke ("PosStop", 1.2f);
			}
		}
	

    public void Generate()
	{
		Debug.Log ("確認");

		num++;

		if (num < partsLoad.Length) {
			parts.Add (Instantiate (partsLoad [num]));　//パーツ生成
			parts [num].SetActive (true);
			parts [num].transform.localPosition = new Vector3 (0, -11.4f, 0);
			parts [num].transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
			parts [num].transform.localRotation = Quaternion.Euler (0, 0, 0);

			//this.time = 10.0f;
		} else {
			Debug.Log ("###");
			screenshot.GetComponent<Screenshot> ().ScreenShotFlag = true;
			//StartCoroutine ("timestop");

		}
	}
		
	IEnumerator timestop(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Finish");
	}

    void PosStop()
	{
		for(int j=0;j<parts.Count;j++)
		{
			Debug.Log ("PosStop");
			parts[j].GetComponent<Rigidbody>().isKinematic = true;
		}
		Triggerfalse ();

	}

	void Triggerfalse()
	{
		
		for(int i=0;i<parts.Count;i++){
			Debug.Log ("Triggerfalse");

			parts[i].GetComponent<Rigidbody> ().isKinematic = false;
		}
	}


	public bool management {
		get { return posmanagement; }
		set { posmanagement = value; }
	}
}

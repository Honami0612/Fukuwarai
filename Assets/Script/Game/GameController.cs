using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject partsPrefab;

    [SerializeField]
    Sprite[] partsSprite;

	[SerializeField]
	List<GameObject> parts = new List<GameObject>();
    [SerializeField]
    List<RuntimeAnimatorController> partsAnimation = new List<RuntimeAnimatorController>();

    [SerializeField]
    Text timerText;


	private string saveFilePath="/ScreenShot";//データの保存先ファイルパス
	private string saveFileName="/screenshot.PNG";//保存ファイル名
	int screenshotnumber=0;
	//int save = PlayerPrefs.GetInt();

    private int num;
    float time = 10.0f;
	private bool posmanagement = false;

    [SerializeField]
    GameObject screenshotPrefab;
    GameObject screenshot;


    private void Start()
    {
        num = 0;

        
        if (GameObject.Find("ScreenShot(Clone)")==null)
        {
            screenshot = Instantiate(screenshotPrefab);
        }
        else
        {
            screenshot = GameObject.Find("ScreenShot(Clone)"); 
        }

        GameObject.Find("parts").GetComponent<SpriteRenderer>().sprite = partsSprite[num];
        GameObject.Find("parts").GetComponent<Animator>().runtimeAnimatorController = partsAnimation[num];
        //StartCoroutine(PartsMove());

    }

	void Update() { 

        this.time -= Time.deltaTime;

        if (this.time < 0)
        {
            Generate();
        }
        else
        {
            this.timerText.text = this.time.ToString("F0");
        }
    

		if (posmanagement==true){
			posmanagement = false;
			Invoke ("PosStop",1.2f);
		}
	}

    public void Generate()
    {
        Debug.Log("確認");

        parts[num].GetComponent<MoveScript>().enabled = false;

        num++;
        if (num < partsSprite.Length)
        {
            parts.Add(Instantiate(partsPrefab) as GameObject);
            parts[num].name = partsSprite[num].name;
            parts[num].GetComponent<SpriteRenderer>().sprite = partsSprite[num];
            if(partsAnimation[num] != null)
            {
                parts[num].GetComponent<Animator>().runtimeAnimatorController = partsAnimation[num];
            }
            
            this.time = 10.0f;
        }
        else
        {
			Screen();
			StartCoroutine ("timestop");
        }
    }

  
	IEnumerator timestop(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("GameFinish");
	}

	public void Screen()
	{
		Debug.Log ("screenshot");
		if(num == partsSprite.Length)
		{
			ScreenCapture.CaptureScreenshot(Application.dataPath+saveFilePath+"/savedata"+screenshotnumber+".PNG",2);
			screenshotnumber++;
			Debug.Log ("screenshotnumber:"+screenshotnumber);
			//PlayerPrefs.SetInt (,screenshotnumber);
		}
	}
	void Awake()
	{
		DontDestroyOnLoad(screenshot.gameObject);
		Debug.Log ("gameobject:"+screenshot.gameObject);
	}

	/*IEnumerator ScreenShot(){
		ScreenCapture.CaptureScreenshot (Application.dataPath + saveFilePath + saveFileName,2);

		yield return new WaitForSeconds (waitTime);
	
	}*/

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



	public bool management{
		get { return posmanagement;}
		set { posmanagement = value; }
	}
}

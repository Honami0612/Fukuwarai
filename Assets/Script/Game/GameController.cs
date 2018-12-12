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

	[SerializeField]
	private GameObject logPanel;//ログパネル
	[SerializeField]
	private GameObject superSizePanel;//解像度パネル
	[SerializeField]
	private Slider superSizeSlider;//解像度レベルスライダー
	[SerializeField]
	private Text superSizeText;//解像度レベルテキスト
	[SerializeField]
	private float waitTime=5f;//スクリーンショットを撮ってからの待ち時間


	private string saveFilePath="/Prijects/ScreenShot";//データの保存先ファイルパス
	private string saveFileName="/screenshot.PNG";//保存ファイル名

    private int num;
    float time = 10.0f;
	private bool posmanagement = false;

    [SerializeField]
    GameObject screenshotPrefab;
    GameObject screenshot;


    //private float scal = 0.2f;
    //private bool scalTrigger = true;



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
		if (!Directory.Exists (Application.dataPath + saveFilePath)) {
			saveFilePath = "";
		}//指定したフォルダがないときはAssetフォルダに保存
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
			//ScreenCapture.CaptureScreenshot (Application.dataPath + "/savedata.PNG");
			//StartCoroutine ("ScreenShot");
			StartCoroutine ("timestop");
        }
    }

  
	IEnumerator timestop(){
		screenshot.GetComponent<Screenshot>().Screen();
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("GameFinish");
	}

	/*IEnumerator ScreenShot(){
		ScreenCapture.CaptureScreenshot (Application.dataPath + saveFilePath + saveFileName, (int)superSizeSlider.value);
		yield return new WaitForSeconds (0.1f);
		logPanel.transform.GetChild (0).GetComponent<Text> ().text = "スクリーンショットを撮りました\n" + Application.dataPath + saveFilePath + saveFileName + "に保存されました";
		logPanel.SetActive (true);
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

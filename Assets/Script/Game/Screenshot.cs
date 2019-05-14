using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;//Exception
using System.IO;//System.IO.FileInfo,System.IO.StreamRender,System.IO.StreamWriter
using System.Text;//Encoding
using UnityEngine.UI;

public class Screenshot : MonoBehaviour {

	private string saveFilePath="/Resources/Sprite/ScreenShot";//データの保存先ファイルパス
	private string saveFileName="/screenshot.PNG";//保存ファイル名


    int save=0;
    private int num;
	StreamWriter sw;
	FileInfo fi;

	private bool screenShotFlag = false;


	private TextAsset csvFile;//CSVファイル

	void Start()
	{
		
		StreamReader sr = new StreamReader (Application.dataPath+"/Resources/ScreenShotnumber.csv");
		while (sr.Peek () > -1) {//reader.Peekがー1になるまで
			string line = sr.ReadLine ();//一行ずつ読み込み
			save=int.Parse(line);
		}

		Debug.Log ("read:" + save);
		/*
		 *Resource.Load:Assets/Resources/下にあるファイルを読み込める
		 *TextAsset:csvFileをreaderにぶち込む
		 *
		 *readerの分割:カンマの区切りでリストにぶち込む
		 *.Peek():は次に読み込める文字列がなくなると-1になる
		 */
	}

	private void Update(){
		if (screenShotFlag == true) {

			screenShotFlag = false;
			StartCoroutine (Screen ());
		}

	
	}

	IEnumerator Screen()
    {
		ScreenCapture.CaptureScreenshot(Application.dataPath+saveFilePath+"/savedata"+save+".PNG");

		save++;
		int a = save - 1;
		Debug.Log ("save:"+save);
		yield return StartCoroutine(logSave ());
		while (File.Exists (Application.dataPath + saveFilePath + "/savedata" + a + ".PNG") == false) {
			yield return null;
		}
		yield return new WaitForSeconds (10f);
		SceneManager.LoadScene ("Start");
		yield break;
    }
	IEnumerator logSave(){
		fi = new FileInfo (Application.dataPath + "/Resources/ScreenShotnumber.csv");//ScreenShotnumber.csvファイルを読み込む
		sw = fi.CreateText ();
		sw.WriteLine (save);
		sw.Flush ();
		sw.Close ();
		Debug.Log("sw:"+sw+"fi"+fi);
		yield break;
	}

	public bool ScreenShotFlag {
		set{ screenShotFlag = value; }
	}

}

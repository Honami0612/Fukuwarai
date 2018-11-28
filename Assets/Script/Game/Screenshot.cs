using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Screenshot : MonoBehaviour {

	private string saveFilePath="/Resouces/Sprite/ScreenShot";//データの保存先ファイルパス
	private string saveFileName="/screenshot.PNG";//保存ファイル名

   /* [SerializeField]
    Sprite[] partsSprite;*/

    int save=0;
    private int num;

	private void Start(){
		
	}

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Screen()
    {
		/*if(num == partsSprite)
        {*/
		Debug.Log ("save:"+save);
			ScreenCapture.CaptureScreenshot(Application.dataPath+saveFilePath+"/savedata"+save+".PNG");
			save++;
		logSave ();
        //}
    }
	public void logSave(){
		StreamWriter sw;
		FileInfo fi;
		fi = new FileInfo (Application.dataPath + "/ScreenShotnumber.csv");
		sw = fi.AppendText ();
		sw.WriteLine (save);
		sw.Flush ();
		sw.Close ();
	}

}

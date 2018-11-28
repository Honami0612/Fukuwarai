using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Screenshot : MonoBehaviour {

    [SerializeField]
    Sprite[] partsSprite;

    int save=0;
    private int num;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Screen()
    {
        if(num == partsSprite.Length)
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath+"/savedata"+save+".PNG");
            save++;
           
        }
    }
}

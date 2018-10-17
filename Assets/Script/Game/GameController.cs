using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject partsPrefab;

    [SerializeField]
    Sprite[] partsSprite;

    private int num;


    private void Start()
    {
        num = 0;

        GameObject.Find("parts").GetComponent<SpriteRenderer>().sprite = partsSprite[num];
    }


    public void Generate()
    {
        Debug.Log("確認");
        num++;
        GameObject mouth = Instantiate(partsPrefab) as GameObject;
        mouth.GetComponent<SpriteRenderer>().sprite = partsSprite[num];
 //       mouth.transform.localPosition = new Vector3(0, 0, 0);	
    }


}

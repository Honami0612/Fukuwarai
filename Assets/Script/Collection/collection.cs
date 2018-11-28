using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collection : MonoBehaviour {

	Sprite[] sprites;//スプライト画像格納用の配列
	int i;

	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite>("/ScreenShot");//フォルダごとまとめて読み込み
		GameObject obj=new GameObject();//空のオブジェクト作成
		for (i = 0; i < sprites.Length; i++) {
			GameObject go = Instantiate (obj, new Vector3 (i, 0, 0), Quaternion.identity)as GameObject;//空のオブジェクト生成、横に並べる
		//	go.AddComponent<SpriteRenderer> ().sprite = Sprite [i];//生成したオブジェクトはTransformのみなのでSpriteRnderをスクリプトから追加、スプライトに配列画像を代入
		}
		Destroy (obj);
	}
	
}

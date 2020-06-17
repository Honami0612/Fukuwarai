using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]

public class MouseController : MonoBehaviour {

    [SerializeField]
	Camera mainCamera;
    private Transform mainCameraTransform = null;

    private const float MaxMagnitude = 2f;
    private Vector3 currentForce = Vector3.zero;
    private Vector3 dragStart = Vector3.zero;

    [SerializeField]
    GameObject arrowObject;
    private Vector3 arrowStartScale;
    private Quaternion arrowStartRotate;
    private float dist = 0.0f;

    [SerializeField]
    MoveScript moveScript;

    [SerializeField]
    GameObject nowTouchPos;
    private bool throwFlag = false;

    private Vector3 leftBottom;
    private Vector3 rightTop;

    private void Awake()
    {
        this.mainCameraTransform = this.mainCamera.transform;
        GetCameraRange();
        arrowStartScale = arrowObject.transform.localScale;
        arrowStartRotate = arrowObject.transform.localRotation;
        nowTouchPos.SetActive(false);
        arrowObject.SetActive(false);
    }


    //カメラの座標取得して移動範囲指定
    private void GetCameraRange()
    {
        leftBottom = mainCamera.ScreenToWorldPoint(Vector3.zero);
        rightTop = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Debug.LogError("カメラ座標（左下）：" + leftBottom.ToString()); 
        Debug.LogError("カメラ座標（右上）" + rightTop.ToString());
    }


    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        position.z = 0;
        return position;
    }


    public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();
        arrowObject.SetActive(true);
        nowTouchPos.SetActive(true);
        throwFlag = true;
	}


    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
        var touchPos = this.GetMousePosition();

        touchPos *= -1;
        touchPos.y += this.mainCameraTransform.localPosition.y * 2;
        touchPos.z = this.gameObject.transform.localPosition.z;
        nowTouchPos.transform.localPosition = touchPos;

        this.currentForce = position - this.dragStart;

        dist = Vector3.Distance(position, this.dragStart); //ここを変えれば大きさの割合が変わる
        arrowObject.transform.position = this.gameObject.transform.localPosition;
        arrowObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, this.currentForce);
        arrowObject.transform.localScale = new Vector3(0.1f, dist, 0.1f);

        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }
    }

    public void OnMouseUp()
    {
        if (throwFlag == true)
        {
            arrowObject.SetActive(false);
            nowTouchPos.SetActive(false);
            moveScript.Flip(this.currentForce * -6f);
        }
        else
        {
            arrowObject.transform.localScale = arrowStartScale;
            arrowObject.transform.localRotation = arrowStartRotate;
        }

    }

    public void ResetData()
    {
        arrowObject.SetActive(true);
        arrowObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        arrowObject.transform.rotation = Quaternion.Euler(0,0,0);
        currentForce = Vector3.zero;
        dragStart = Vector3.zero;
        dist = 0.0f;
    }


    public void SetParts(MoveScript m)
    {
        moveScript = m;
    }


    private void OnTriggerExit(Collider touchArea)//投げられる範囲外
    {
        if (touchArea == nowTouchPos.GetComponent<BoxCollider>()) throwFlag = false;
    }


    private void OnTriggerStay(Collider touchArea)//投げられる範囲内
    {
        if (touchArea == nowTouchPos.GetComponent<BoxCollider>()) throwFlag = true;
    }



    public Vector3 SetleftleftBottom
    {
        get { return leftBottom; }
    }

    public Vector3 SetrightTop
    {
        get { return rightTop; }
    }


}


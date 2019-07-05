using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]

public class MouseController : MonoBehaviour {

	private Camera mainCamera = null;
    private Transform mainCameraTransform = null;

    private const float MaxMagnitude = 2f;
    private Vector3 currentForce = Vector3.zero;
    public Vector3 dragStart = Vector3.zero;

    public GameObject arrowPos;
    private Vector3 arrowStartScale;
    private Quaternion arrowStartRotate;
    private float dist = 0.0f;

    public MoveScript moveScript;

    [SerializeField]
    GameObject nowTouchPos;
    private bool throwFlag = false;

    private void Awake()
    {
		this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
        arrowStartScale = arrowPos.transform.localScale;
        arrowStartRotate = arrowPos.transform.localRotation;
        nowTouchPos.SetActive(false);
        arrowPos.SetActive(false);
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
        arrowPos.SetActive(true);
        nowTouchPos.SetActive(true);
        throwFlag = true;
		Debug.Log ("MouseDown");
	}

    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
        var touchPos = this.GetMousePosition();

        // クリックしている場所　消すと反転
        touchPos *= -1;
        touchPos.y += this.mainCameraTransform.localPosition.y * 2;
        touchPos.z = this.gameObject.transform.localPosition.z;
        nowTouchPos.transform.localPosition = touchPos;


        this.currentForce = position - this.dragStart;

        dist = Vector3.Distance(position, this.dragStart); //ここを変えれば大きさの割合が変わる
        arrowPos.transform.position = this.gameObject.transform.localPosition;
        arrowPos.transform.rotation = Quaternion.FromToRotation(Vector3.down, this.currentForce);
        arrowPos.transform.localScale = new Vector3(0.1f, dist, 0.1f);


        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }
    }

    public void OnMouseUp()
    {
        if (throwFlag == true)
        {
            arrowPos.SetActive(false);
            nowTouchPos.SetActive(false);
            moveScript.Flip(this.currentForce * -6f);
        }
        else
        {
            arrowPos.transform.localScale = arrowStartScale;
            arrowPos.transform.localRotation = arrowStartRotate;
        }
    }

    public void ResetData()
    {
        arrowPos.SetActive(true);
        arrowPos.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        arrowPos.transform.rotation = Quaternion.Euler(0,0,0);
        currentForce = Vector3.zero;
        dragStart = Vector3.zero;
        dist = 0.0f;
    }

    public void SetParts(MoveScript m){
        moveScript = m;
    }

    private void OnTriggerExit(Collider touchArea)
    {
        if (touchArea == nowTouchPos.GetComponent<BoxCollider>())
        {
            Debug.Log("投げられる範囲を超えました");
            throwFlag = false;
        }
    }
    private void OnTriggerStay(Collider touchArea)
    {
        if (touchArea == nowTouchPos.GetComponent<BoxCollider>())
        {
            Debug.Log("投げられる範囲です");
            throwFlag = true;
        }
    }
}


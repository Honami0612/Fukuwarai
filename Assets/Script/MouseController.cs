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
    private float dist = 0.0f;

    private MoveScript moveScript;

    private void Awake()
    {
        this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
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
		Debug.Log ("MouseDown");
	}

    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
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
        arrowPos.SetActive(false);
		moveScript.Flip(this.currentForce * -6f);
    }

	//[Command]
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
}


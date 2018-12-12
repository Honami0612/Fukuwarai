using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]

public class ArrowScript : MonoBehaviour
{
    /// <summary>
    /// 物理剛体
    /// </summary>
    private Rigidbody physics = null;

    /// <summary>
    /// 発射方向
    /// </summary>
    [SerializeField]
    private LineRenderer direction = null;

    /// <summary>
    /// 最大付与力量
    /// </summary>
    private const float MaxMagnitude = 2f;

    /// <summary>
    /// 発射方向の力
    /// </summary>
    private Vector3 currentForce = Vector3.zero;

    /// <summary>
    /// メインカメラ
    /// </summary>
    private Camera mainCamera = null;

    /// <summary>
    /// メインカメラ座標
    /// </summary>
    private Transform mainCameraTransform = null;

    /// <summary>
    /// ドラッグ開始点
    /// </summary>
    private Vector3 dragStart = Vector3.zero;

    [SerializeField]
    GameObject arrowPos;

    float dist;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Awake()
    {
        this.physics = this.GetComponent<Rigidbody>();
        this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
    }

    private void Start()
    {
        direction = this.gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        arrowPos = this.gameObject.transform.GetChild(1).gameObject;
        arrowPos.SetActive(false);
    }

    /// <summary>
    /// マウス座標をワールド座標に変換して取得
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }

    /// <summary>
    /// ドラック開始イベントハンドラ
    /// </summary>
    public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();
        arrowPos.SetActive(true);

        this.direction.enabled = true;
        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position);
    }

    /// <summary>
    /// ドラッグ中イベントハンドラ
    /// </summary>
    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
        this.currentForce = position - this.dragStart;

        dist = Vector3.Distance(position, this.dragStart); //ここを変えれば大きさの割合が変わる
        arrowPos.transform.position = this.gameObject.transform.localPosition;
        arrowPos.transform.rotation = Quaternion.FromToRotation(Vector3.down, this.currentForce);
        arrowPos.transform.localScale = new Vector3(0.2f, dist, 0.2f);


        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }

        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position + this.currentForce);
    }

    /// <summary>
    /// ドラッグ終了イベントハンドラ
    /// </summary>
    public void OnMouseUp()
    {
        arrowPos.SetActive(false);
        this.direction.enabled = false;
        this.Flip(this.currentForce * -6f);
    }

    /// <summary>
    /// ボールをはじく
    /// </summary>
    /// <param name="force"></param>
    public void Flip(Vector3 force)
    {
        // 瞬間的に力を加えてはじく
        this.physics.AddForce(force, ForceMode.Impulse);
        this.gameObject.GetComponent<ArrowScript>().enabled = false;
    }
}
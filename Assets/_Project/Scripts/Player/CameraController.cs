﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public PlayerController pc;
    public PlayerCharacterController pcc;
    public float PosSpeed = 7.5f;
    public Vector3 gameplayOffset;
    public Vector3 gameplayDirection;
    public Vector3 gameplayDirectionUp;
    public Vector3 gameplayDirectionDown;
    public Vector3 gameplayDirectionLeft;
    public Vector3 gameplayDirectionRight;
    public Camera cam;
    public float moveSpeed = 1;
    public Vector2Int resolution;
    public int resolutionMin;
    Vector2 moveAxis;
    private void Awake()
    {
        if (!cam)
            cam = GetComponent<Camera>();
        GameManager.Instance.cameraController = this;
        UpdateResolution();
        cam.transform.position = pcc.transform.position + gameplayOffset;
        cam.transform.localRotation = Quaternion.Euler(gameplayDirection);
    }

    void LateUpdate()
    {
        moveAxis = Vector2.Lerp(moveAxis, pc.MoveAxis, Time.deltaTime * moveSpeed);
        Vector3 desiredDirection = gameplayDirection;
        desiredDirection = Vector3.Lerp(desiredDirection, gameplayDirectionUp, Mathf.Clamp(moveAxis.y, 0, 1));
        desiredDirection = Vector3.Lerp(desiredDirection, gameplayDirectionDown, Mathf.Clamp(-moveAxis.y, 0, 1));
        desiredDirection = Vector3.Lerp(desiredDirection, gameplayDirectionRight, Mathf.Clamp(moveAxis.x, 0, 1));
        desiredDirection = Vector3.Lerp(desiredDirection, gameplayDirectionLeft, Mathf.Clamp(-moveAxis.x, 0, 1));
        cam.transform.position = Vector3.Lerp(cam.transform.position, pcc.transform.position + gameplayOffset, Mathf.Clamp(Time.deltaTime * PosSpeed, 0, 1));
        cam.transform.localRotation = Quaternion.Euler(desiredDirection);
    }

    public void UpdateResolution()
    {
        resolution.x = cam.pixelWidth;
        resolution.y = cam.pixelHeight;
        resolutionMin = Mathf.Min(resolution.x, resolution.y);
        GameManager.Instance.resolution = resolution;
        GameManager.Instance.resolutionF = resolution;
        GameManager.Instance.resolutionHalf = resolution / 2;
        GameManager.Instance.resolutionHalfF = resolution / 2;
        GameManager.Instance.resolutionMin = resolutionMin;
        GameManager.Instance.resolutionMinF = resolutionMin;
    }
}

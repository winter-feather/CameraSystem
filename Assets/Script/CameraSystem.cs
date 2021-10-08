using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform target;
    public Camera targetCamera;
    [HideInInspector]
    public Vector3 offset;
    Vector3 targetPos;
    //-----------------------------------------------
    [HideInInspector]
    public float hAngle, vAngle;
    public Vector3 cameraDefaultDir;
    Quaternion cameraDefaultRot;
    Quaternion targetRot;
    //-----------------------------------------------
    public float nzoom, fzoom;
    [HideInInspector]
    public float currentzoom;
    [HideInInspector]
    private float targetzoom;

    public float Targetzoom { get => targetzoom; set => targetzoom = Mathf.Clamp(value, nzoom, fzoom); }

    //-----------------------------------------------
    //public Transform boundMin, boundMax;
    private void Awake()
    {
        Init();
    }
    void Init()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        if (cameraDefaultDir == Vector3.zero)
        {
            cameraDefaultDir = Vector3.up;
        }
        cameraDefaultRot = Quaternion.LookRotation(-cameraDefaultDir);
    }

    void ZoomUpdate()
    {
        currentzoom = Mathf.Lerp(currentzoom, Targetzoom, Time.deltaTime * 10);
    }

    void PosUpdate()
    {
        targetRot = Quaternion.Slerp(targetRot, Quaternion.Euler(hAngle, vAngle, 0),0.1f);
        targetPos = target.transform.position + offset; 
    }

    void CameraUpdate()
    {
        targetCamera.transform.position = targetPos + targetRot * cameraDefaultDir * currentzoom;
        targetCamera.transform.rotation = targetRot * cameraDefaultRot;
    }
    float clipPlaneValue = 0.01f;
    void ChangeCamerNearPlane(float inputValue) {
        clipPlaneValue += inputValue;
        clipPlaneValue = Mathf.Clamp(clipPlaneValue, 0.001f, 20f);
        UpdateClipPlane();
    }

    void UpdateClipPlane() {
        targetCamera.nearClipPlane = clipPlaneValue;
    }

    void Update()
    {
        InputUpdate();
        ZoomUpdate();
        PosUpdate();
        CameraUpdate();
    }

    public void InputUpdate() {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                hAngle -= Input.GetAxis("Mouse Y") * 2;
                hAngle = Mathf.Clamp(hAngle, -179, -1);
                vAngle += Input.GetAxis("Mouse X") * 2;
            }
            Targetzoom -= Input.mouseScrollDelta.y * 0.25f;
            ChangeCamerNearPlane(Input.GetAxis("ChangeNP"));
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetMouseButton(0))
            {
                hAngle -= Input.GetAxis("Mouse Y") * 2;
                hAngle = Mathf.Clamp(hAngle, -89, -1);
                vAngle += Input.GetAxis("Mouse X") * 2;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        offset = Vector3.zero;
    }
}


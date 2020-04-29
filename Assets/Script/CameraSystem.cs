using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    //-------------------------------------------------------------------
    //public List<Transform> targets;
    //public Vector3 TargetPos
    //{
    //    get
    //    {
    //        Vector3 targetPos = Vector3.zero;
    //        for (int i = 0; i < targets.Count; i++)
    //        {
    //            targetPos += targets[i].position;
    //        }
    //        targetPos = targetPos / targets.Count;
    //        return targetPos;
    //    }
    //}
    //-------------------------------------------------------------------
    //private Vector3 bound;
    //public Vector3 Bound
    //{
    //    get
    //    {
    //        return bound;
    //    }

    //    set
    //    {
    //        bound = value;
    //    }
    //}
    public Transform target, targetCamera;
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
    public float currentzoom, targetzoom;
    //-----------------------------------------------
    public Transform boundMin, boundMax;
    private void Awake()
    {
        Init();
    }
    void Init()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main.transform;
        }
        if (cameraDefaultDir == Vector3.zero)
        {
            cameraDefaultDir = Vector3.up;
        }
        cameraDefaultRot = Quaternion.LookRotation(-cameraDefaultDir);
    }

    void ZoomUpdate()
    {
        currentzoom = Mathf.Lerp(currentzoom, targetzoom, Time.deltaTime * 10);
    }

    void PosUpdate()
    {
        targetRot = Quaternion.Euler(hAngle, vAngle, 0);
        targetPos = target.transform.position + offset; ;

    }

    void CameraUpdate()
    {
        targetCamera.position = targetPos + targetRot * cameraDefaultDir * currentzoom;
        targetCamera.rotation = targetRot * cameraDefaultRot;
    }

    void BoundUpdate()
    {
        if (boundMin == null || boundMax == null) return;
        targetPos.x = Mathf.Clamp(targetPos.x, boundMin.position.x, boundMax.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, boundMin.position.y, boundMax.position.y);
        targetPos.z = Mathf.Clamp(targetPos.z, boundMin.position.z, boundMax.position.z);
        offset = targetPos - target.transform.position;
    }
    void Update()
    {
        //hAngle += Input.GetAxis("Vertical");
        //vAngle += Input.GetAxis("Horizontal");
        offset.x += Input.GetAxis("Horizontal");
        offset.z += Input.GetAxis("Vertical");


        targetzoom += Input.mouseScrollDelta.y;
        targetzoom = Mathf.Clamp(targetzoom, nzoom, fzoom);

        ZoomUpdate();
        PosUpdate();
        BoundUpdate();
        CameraUpdate();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        offset = Vector3.zero;
    }
}


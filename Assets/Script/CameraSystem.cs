using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {
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
    Vector3 targetPos;
    Quaternion targetRot;
    [HideInInspector]
    public Vector3 offset;
    public float nzoom, fzoom;
    [HideInInspector]
    public float currentzoom, targetzoom;
    [HideInInspector]
    public float hAngle, vAngle;
    public Vector3 cameraDefaultDir;
    Quaternion cameraDefaultRot;
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

    void ZoomUpdate() {
        currentzoom = Mathf.Lerp(currentzoom,targetzoom,Time.deltaTime*10);
    }

    void PosUpdate() {
        targetRot = Quaternion.Euler(hAngle, vAngle, 0);
        targetPos = target.transform.position + targetRot * cameraDefaultDir * currentzoom + offset;
        targetCamera.position =  targetPos;
        targetCamera.rotation = targetRot * cameraDefaultRot;
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
    }

    public void SetTarget(Transform target) {
        this.target = target;
        offset = Vector3.zero;
    }
}


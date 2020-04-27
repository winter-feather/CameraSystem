using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Vector3 offsetPostion = Vector3.zero;
    public Transform cmaera;
    public Transform target;
    // Use this for initialization
    private void Start()
    {
        transform.position = target.position + offsetPostion;   
    }
    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Mouse X") * 0.05f;
        //float v = Input.GetAxis("Mouse Y") * 0.05f;
        float h = Input.GetAxis("Horizontal") * 0.05f;
        float v = Input.GetAxis("Vertical") * 0.05f;
        offsetPostion.x += h;
        offsetPostion.y += v;
        //if (Input.GetMouseButton(2))
        //{
        //    p.x += h;
        //    p.y += v;
        //}
        //transform.position = target.position + Quaternion.Euler(-transform.parent.rotation.eulerAngles) * cmaera.rotation * -offsetPostion;// 
        transform.position = target.position + cmaera.rotation * -offsetPostion;// 
    }

    public void SetTarget(Transform t) {
        target = t;
        offsetPostion = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IM : MonoBehaviour
{
    float doubleClickTime;
    public float doubleClickCD;
    public Vector3 om, md;
    public TC tc;
    //public GameObject so;
    public Action<IM> onDoubleClick, onPressing, onMouseDown, onMouseUp;
    public int ya;
    // Use this for initialization
    void Start()
    {
        om = Input.mousePosition;
        tc.a = Quaternion.identity;
        tc.b = Quaternion.identity;
        onDoubleClick += (e) =>
        {
            //if (so!=null)
            //{
            //    for (int i = 0; i < tc.t.childCount; i++)
            //    {
            //        tc.t.transform.GetChild(i).gameObject.SetActive(false);
            //    }
            //    so.SetActive(true);
            //}
            //else
            //{
            //    for (int i = 0; i < tc.t.childCount; i++)
            //    {
            //        tc.t.transform.GetChild(i).gameObject.SetActive(true);
            //    }
            //}
        };
    }

    // Update is called once per frame
    void Update()
    {
        DoubleCheck();
        MoseMoveCheck();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(tc.tc.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
        //    {
        //        so = hit.collider.gameObject;
        //    }
        //    else
        //    {
        //        so = null;
        //    }
        //}
        if (Input.GetMouseButton(0))
        {
            tc.RH(-md.x);
            tc.RV(ya * md.y);
        }
        tc.RZ(Input.mouseScrollDelta.y);
    }

    void DoubleCheck()
    {
        doubleClickTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (doubleClickTime > doubleClickCD)
            {
                doubleClickTime = 0;
            }
            else
            {
                if (onDoubleClick != null)
                {
                    onDoubleClick.Invoke(this);
                }
            }
        }
    }

    void MoseMoveCheck()
    {
        md = om - Input.mousePosition;
        om = Input.mousePosition;
    }


}

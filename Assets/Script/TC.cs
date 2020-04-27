using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC : MonoBehaviour
{
    public float hs, vs, zs;
    public Quaternion a, b;
    public Transform t, tc;
    public float z, tz;
    public float nd = 0.15f, fd = 0.5f;
    protected Vector3 tt;
    public float Z
    {
        get
        {
            return z;
        }

        set
        {
            z = Mathf.Clamp(value, nd, fd);
        }
    }
    public void Start()
    {
        tt = t.position;
        tz = z;
    }
    public virtual void LateUpdate()
    {
        tt = t.position; //Vector3.Lerp(tt, t.position, 5*Time.deltaTime);
        Z = tz;// Mathf.Lerp(Z, tz, 5 * Time.deltaTime);// 
        tc.transform.position = tt + (a * b * -Vector3.forward * Z);
        tc.transform.rotation = a * b; //* Quaternion.Euler(transform.up*180);
    }
    public void SetT(Transform t)
    {

    }
    public virtual void RH(float angle)
    {
        a = Quaternion.Euler(Vector3.up * angle * hs) * a;
    }

    public virtual void RV(float angle)
    {
        b = Quaternion.Euler(Vector3.right * angle * vs) * b;
    }

    public void RZ(float md)
    {
        tz -= md * zs;
    }
}

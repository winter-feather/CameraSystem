using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform cameraTarget;
    public Transform minBox, maxBox;
    public float moveSpeedScale;
    Vector3 dir;
    void Start()
    {

    }
    void Update()
    {
        dir = Vector3.ProjectOnPlane(cameraTarget.forward, Vector3.up).normalized;
        InputUpdate();
    }

    void InputUpdate() {
        MoveHorizontal(Input.GetAxis("Horizontal"));
        MoveForward(Input.GetAxis("Vertical"));
        MoveUp(Input.GetAxis("UpAndDown"));
    }

    Vector3 EdgeCheck(Vector3 a) {
        a.x = Mathf.Clamp(a.x, minBox.position.x, maxBox.position.x);
        a.y = Mathf.Clamp(a.y, minBox.position.y, maxBox.position.y);
        a.z = Mathf.Clamp(a.z, minBox.position.z, maxBox.position.z);
        return a;
    }

    void MoveHorizontal(float value) {
        transform.position += value * (Quaternion.AngleAxis(90, Vector3.up) * dir) * moveSpeedScale;
    }

    void MoveForward(float value) {
        transform.position += value * dir * moveSpeedScale;
    }

    void MoveUp(float value) {
        transform.position += value * Vector3.up * moveSpeedScale;
    }

    private void LateUpdate()
    {
        transform.position = EdgeCheck(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRotation : MonoBehaviour
{
    public Rigidbody CarRigid;
    public float RotateRatio;

    void Update()
    {
        GameObject pointerWrapper = transform.gameObject;
        RectTransform pointerWrapperRT = pointerWrapper.GetComponent<RectTransform>();

        pointerWrapperRT.localRotation = Quaternion.Euler(0f, 0f, - 60 - getVelocity(CarRigid.velocity) * RotateRatio);
    }

    private float getVelocity(Vector3 v){
        float velocity = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        return velocity;
    }
}

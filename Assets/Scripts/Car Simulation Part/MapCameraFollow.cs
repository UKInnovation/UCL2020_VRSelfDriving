using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    public Transform carT;

    // Update is called once per frame
    void Update()
    {
        double y = carT.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(90 ,(float)y  ,0);
        transform.position = carT.transform.position + new Vector3(0, 100, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }
}

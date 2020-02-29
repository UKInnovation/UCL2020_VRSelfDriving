using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.parent != null)
        {
            transform.position = new Vector3(transform.parent.position.x, 3, 0.3f);
            transform.rotation = Quaternion.identity;
        }
        
    }
}

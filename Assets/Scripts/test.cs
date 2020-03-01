using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody rb;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
    void FixedUpdate()
    {
        if(transform.parent != null)
        {
            transform.position = new Vector3(transform.parent.position.x, 3, 0.3f);
            transform.rotation = Quaternion.identity;
        }
        
    }
}

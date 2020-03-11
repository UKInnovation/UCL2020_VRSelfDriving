using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationIcon : MonoBehaviour
{
    private Camera MapCamera;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        MapCamera = GameObject.Find("MapCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1) * (MapCamera.orthographicSize / 50);
    }
}

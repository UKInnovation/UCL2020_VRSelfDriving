using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _carT;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_carT.position.x, 100, _carT.position.z);
    }
}

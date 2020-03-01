using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
//     Start is called before the first frame update
    void Start () {
         Renderer[] renderers = GetComponentsInChildren<Renderer>();
         foreach (Renderer r in renderers)
         {
             r.material.renderQueue = 4999;
         }
     }
}

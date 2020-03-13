using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerScript : MonoBehaviour
{
    public GameObject decalPrefab = null;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") == true)
        {
            //get mouseclick pisition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) == true)
            {
                SpawnDecal(hit);
            }
        }
    }

    private void SpawnDecal(RaycastHit hit)
    {
        GameObject decal = Instantiate(decalPrefab);

        //1. face decal same as surface
        decal.transform.forward = hit.normal * -1f;

        //2. position on surface
        decal.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

        //3/ just above surface (so dose not conflict for visibility)
        decal.transform.Translate(Vector3.forward * -0.01f);
    }

    private void OnDrawGizmos()
    {
        // visualize  the normal of the decal in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, 0.2f);
        Gizmos.DrawLine(hit.point, hit.point + hit.normal);
        Gizmos.DrawSphere(hit.point + hit.normal, 0.1f);
    }
}

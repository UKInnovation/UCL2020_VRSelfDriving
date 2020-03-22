using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decal_demo : MonoBehaviour
{
    public GameObject cube;
    public Material sticker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDecal()
    {
        print(cube.name);
        Renderer cubeRenderer = cube.GetComponentInChildren<Renderer>();
        print(cubeRenderer);
        //Material[] materials = new Material[]
        //{
        //    Resources.Load("Assets/Resources/Models/Cube0") as Material,
        //};
        print(sticker);
        cubeRenderer.material = sticker;
    }
}

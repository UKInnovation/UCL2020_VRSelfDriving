using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private float radio;
    [SerializeField]
    private Rigidbody car;
    private Image bar;

    void Awake()
    {
        bar = transform.Find("Progression Bar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = car.velocity.magnitude * radio;
    }
}

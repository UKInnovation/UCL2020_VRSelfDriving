using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class FixAxisGrabber : OVRGrabber
{
    [SerializeField]
    private bool FixXAxis;
    [SerializeField]
    private bool FixYAxis;
    [SerializeField]
    private bool FixZAxis;
    [SerializeField]
    private bool FixRotation;

}

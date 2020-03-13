using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsControl : MonoBehaviour
{
    public int OpenAmount = 70;
    private float WindowsOpenPos = 0;

    void Update()
    {
        if((transform.localPosition.z - WindowsOpenPos) > 0.05 || (transform.localPosition.z - WindowsOpenPos) < -0.05){
            if((transform.localPosition.z - WindowsOpenPos) > 0){
                transform.localPosition = transform.localPosition + new Vector3(0, 0, -10f * Time.deltaTime);
            }else{
                transform.localPosition = transform.localPosition + new Vector3(0, 0, 10f * Time.deltaTime);
            }
        }
    }

    public void open(){
        WindowsOpenPos = -OpenAmount;
    }
    public void close(){
        WindowsOpenPos = 0;
    }
}

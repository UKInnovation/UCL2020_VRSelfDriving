using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempControl : MonoBehaviour
{
    private int currentTemp = 25;
    public GameObject Temp;
    void Start()
    {
        Temp.GetComponent<Text>().text = currentTemp.ToString();
    }

    public void increaseTemp(){
        if(currentTemp < 30){
            currentTemp += 1;   
        }
        Temp.GetComponent<Text>().text = currentTemp.ToString();
    }

    
    public void decreaseTemp(){
        if(currentTemp > 16){
            currentTemp -= 1;
        }
        Temp.GetComponent<Text>().text = currentTemp.ToString();
    }
}

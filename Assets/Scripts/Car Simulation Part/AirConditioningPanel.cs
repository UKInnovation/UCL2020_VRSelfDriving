using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoyouOculusFramework;
using UnityEngine.UI;

public class AirConditioningPanel : MonoBehaviour
{
    [SerializeField]
    private SelectableButton[] Buttons;

    void Start()
    {
        switchToMode(0);
    }
    public void switchToMode(int num)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i == num)
            {
                Buttons[i].Select();
            }
            else
            {
                Buttons[i].CancelSelect();
            }
        }
    }

}

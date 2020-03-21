using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoyouOculusFramework;
using UnityEngine.UI;

public class AirConditioningPanel : MonoBehaviour
{
    [SerializeField]
    private SelectableButton[] Buttons;
    // [SerializeField]
    // private HandTrackingButton Mode2Button;
    // [SerializeField]
    // private HandTrackingButton Mode3Button;
    // [SerializeField]
    // private HandTrackingButton Mode4Button;
    // [SerializeField]
    // private HandTrackingButton Mode5Button;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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

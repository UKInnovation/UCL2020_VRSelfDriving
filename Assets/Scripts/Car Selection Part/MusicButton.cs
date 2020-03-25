using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    private Text text;
    private int index;
    private void Awake()
    {
        text = this.GetComponentInChildren<Text>();
    }

    public void OnClick()
    {
        if (index == 0)
        {
            text.text = "Play Music";
            index = 1;
        }
        else
        {
            text.text = "Stop Music";
            index = 0;
        }
    }
}

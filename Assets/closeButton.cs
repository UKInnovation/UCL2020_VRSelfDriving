using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeButton : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;
    private int index;

    public void OnClick()
    {
        if (index == 0)
        {
            image.sprite = sprites[1];
            index = 1;
        }
        else
        {
            image.sprite = sprites[0];
            index = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class SelectableButton : HandTrackingButton, Selectable
    {
        public Text text;
        public void Select()
        {

            text.text = "here";
            RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].color = new Color(0,255,248);
            }
        }
        public void CancelSelect()
        {
            RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].color = new Color(0,99,96);
            }
        }
    }
}

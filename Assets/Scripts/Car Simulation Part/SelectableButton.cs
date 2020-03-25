using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class SelectableButton : HandTrackingButton, Selectable
    {
        public void Select()
        {

            RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].color = new Color(0,1,1);
            }
        }
        public void CancelSelect()
        {
            RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].color = new Color(0,0.3f,0.3f);
            }
        }
    }
}

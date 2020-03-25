using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite playIcon;
        [SerializeField]
        private Sprite pauseIcon;
        
        public void toPlayIcon()
        {
            transform.GetComponent<Image>().sprite = playIcon;
        }
        public void toPausecIcon()
        {
            transform.GetComponent<Image>().sprite = pauseIcon;
        }
    }
}


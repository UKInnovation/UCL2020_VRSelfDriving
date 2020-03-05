using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class SurroundingRotators : MonoBehaviour
    {
        private Image[] images;

        private void Awake() 
        {
            images = transform.GetComponentsInChildren<Image>();    
        }

        void Start() {
            // deActive();     
        }
        public void setActive()
        {
            foreach(Image image in images)
            {
                image.enabled = true;
            }
        }

        
        public void deActive()
        {
            foreach(Image image in images)
            {
                image.enabled = false;
            }
        }
    }

}
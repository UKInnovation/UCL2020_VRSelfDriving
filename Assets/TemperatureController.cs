using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace YoyouOculusFramework
{
    public class TemperatureController : HandRiseUpListner
    {
        private float Temperature;
        [SerializeField]
        private Text TemperatureT;
        private float timer;

        protected override void OnHandRisingUp(float amount)
        {
            Temperature += amount * 20;
            TemperatureT.text = ((int)Temperature).ToString();
            // TemperatureT.text = "here2";
        }

        public override void AttachToController()
        {
            faceUpGestureListner.OnRisingUp += OnHandRisingUp;
            timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            float timeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer;
            if(timer > 5000){
                DeAttachToController();
            }
        }

    }
}

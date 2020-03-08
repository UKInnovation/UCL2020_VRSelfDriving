using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace YoyouOculusFramework
{
    public class TemperatureController : HandRiseUpListner
    {
        private float Temperature = 20f;
        [SerializeField]
        private Text TemperatureT;
        private float timer;
        [SerializeField]
        private AutoRotate surroundCircle;
        private bool Attached = false;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            faceUpGestureListner = FaceUpGestureController.INSTANCE;
            TemperatureT.text = ((int)Temperature).ToString();
        }
        protected override void OnHandRiseORFall(float RHamount, float LHamount)
        {
            Temperature += RHamount * 20 + LHamount * 20;
            if(Temperature > 30){
                Temperature = 30;
            }
            else if(Temperature < 16){
                Temperature = 16;
            }
            TemperatureT.text = ((int)Temperature).ToString();
        }

        public override void AttachToController()
        {
            faceUpGestureListner.OnRisingOrFalling += OnHandRiseORFall;
            surroundCircle.ChangeRotateSpeed(120);
            timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Attached = true;
            
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(Attached)
            {
                float timeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer;
                if(timeElapsed > 5000){
                    DeAttachToController();
                    surroundCircle.ChangeRotateSpeed(30);
                    Attached = false;
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace YoyouOculusFramework
{
    public class FanLevelController : HandRiseUpListner
    {
        private float FanLevel = 100f;
        [SerializeField]
        private Text FanLevelT;
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
            FanLevelT.text = ((int)FanLevel).ToString() + "%";
        }
        protected override void OnHandRiseORFall(float RHamount, float LHamount)
        {
            FanLevel += RHamount * 20 + LHamount * 20;
            if(FanLevel > 100){
                FanLevel = 20;
            }
            else if(FanLevel < 0){
                FanLevel = 0;
            }
            FanLevelT.text = ((int)FanLevel).ToString() + "%";
        }

        public override void AttachToController()
        {
            faceUpGestureListner.OnRisingOrFalling += OnHandRiseORFall;
            // surroundCircle.ChangeRotateSpeed(120);
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

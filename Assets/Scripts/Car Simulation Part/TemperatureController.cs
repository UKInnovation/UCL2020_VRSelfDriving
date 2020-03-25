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
        private long timer;
        [SerializeField]
        private AutoRotate surroundCircle;
        private bool Attached = false;

        void Start()
        {
            faceUpGestureListner = FaceUpGestureController.INSTANCE;
            TemperatureT.text = string.Format("{0}\u00B0C", (int)Temperature);
        }
        protected override void OnHandRiseORFall(float RHamount, float LHamount)
        {
            Temperature += RHamount * 20 + LHamount * 20;
            if (Temperature > 30)
            {
                Temperature = 30;
            }
            else if (Temperature < 16)
            {
                Temperature = 16;
            }
            TemperatureT.text = string.Format("{0}\u00B0C", (int)Temperature);
        }

        public override void AttachToController()
        {
            faceUpGestureListner.OnRisingOrFalling += OnHandRiseORFall;
            surroundCircle.ChangeRotateSpeed(120);
            timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Attached = true;

        }

        void Update()
        {
            if (Attached)
            {
                long timeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer;
                Debug.Log(timeElapsed);
                if (timeElapsed > 5000)
                {
                    DeAttachToController();
                    surroundCircle.ChangeRotateSpeed(30);
                    Attached = false;
                }
            }
        }

    }
}

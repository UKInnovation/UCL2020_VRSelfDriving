using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YoyouOculusFramework
{
    public class HandRiseUpListner : MonoBehaviour
    {
        protected FaceUpGestureController faceUpGestureListner;
        void Start()
        {
            faceUpGestureListner = FaceUpGestureController.INSTANCE;
        }

        protected virtual void OnHandRiseORFall(float RHamount, float LHamount)
        {

        }

        public virtual void AttachToController()
        {
            faceUpGestureListner.OnRisingOrFalling += OnHandRiseORFall;
        }

        public virtual void DeAttachToController()
        {
            faceUpGestureListner.OnRisingOrFalling -= OnHandRiseORFall;
        }

    }
}

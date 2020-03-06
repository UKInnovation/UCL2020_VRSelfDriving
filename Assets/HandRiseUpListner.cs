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

        protected virtual void OnHandRisingUp(float amount)
        {

        }

        public virtual void AttachToController()
        {
            faceUpGestureListner.OnRisingUp += OnHandRisingUp;
        }

        public virtual void DeAttachToController()
        {
            faceUpGestureListner.OnRisingUp -= OnHandRisingUp;
        }

    }
}

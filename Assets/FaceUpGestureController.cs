using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class FaceUpGestureController : MonoBehaviour
    {
        public static FaceUpGestureController INSTANCE;
        public Hands Hands;
        private GestureListener gestureListener;
        private Transform LeftHandT;
        private Transform RightHandT;
        private Vector3 currentHandPos;
        private Vector3 prevHandPos;
        private bool isCurrentHandFaceUp;
        private bool isPrevHandFaceUp;
        public event UnityAction<float> OnRisingUp;
        public Text text;
  
        void Awake() 
        {
            INSTANCE = this;    
        }
        void Start()
        {
            LeftHandT = Hands.Instance.LeftHand.gameObject.transform;
            RightHandT = Hands.Instance.RightHand.gameObject.transform;
            gestureListener = GestureListener.INSTANCE;

            currentHandPos = RightHandT.transform.position;
            isCurrentHandFaceUp = gestureListener.HasGesture[(int)GestureListener.Gesture.FaceUp];
        }

        // Update is called once per frame
        void Update()
        {
            prevHandPos = currentHandPos;
            isPrevHandFaceUp = isCurrentHandFaceUp;
            currentHandPos = RightHandT.transform.position;
            isCurrentHandFaceUp = gestureListener.HasGesture[(int)GestureListener.Gesture.FaceUp];

            if(OnRisingUp != null && isPrevHandFaceUp && isCurrentHandFaceUp && currentHandPos.y - prevHandPos.y > 0)
            // if(OnRisingUp != null && currentHandPos.y - prevHandPos.y > 0)
            {
                text.text = "here";
                OnRisingUp(currentHandPos.y - prevHandPos.y);
            }
        }
    }
}
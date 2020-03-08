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
        private Vector3 RHcurrentPos;
        private Vector3 RHprevPos;
        private bool RHisFaceUp;
        private bool RHisFaceDwon;
        private Vector3 LHcurrentPos;
        private Vector3 LHprevPos;
        private bool LHisFaceUp;
        private bool LHisFaceDwon;
        public event UnityAction<float, float> OnRisingOrFalling;
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

            RHcurrentPos = RightHandT.transform.position;
            LHcurrentPos = LeftHandT.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(OnRisingOrFalling != null)
            {
                text.text = "amHere";
                RHprevPos = RHcurrentPos;
                LHprevPos = LHcurrentPos;

                RHcurrentPos = RightHandT.transform.position;
                LHcurrentPos = LeftHandT.transform.position;

                RHisFaceUp = gestureListener.HasGesture[(int)GestureListener.Gesture.RHFaceUp];
                LHisFaceUp = gestureListener.HasGesture[(int)GestureListener.Gesture.LHFaceUp];

                RHisFaceDwon = gestureListener.HasGesture[(int)GestureListener.Gesture.RHFaceDown];
                LHisFaceDwon = gestureListener.HasGesture[(int)GestureListener.Gesture.LHFaceDown];

                float RHamount = 0;
                float LHamount = 0;

                if(RHisFaceUp)
                {
                    if(RHcurrentPos.y - RHprevPos.y > 0)
                    {
                        RHamount = RHcurrentPos.y - RHprevPos.y;
                    }
                }
                else if(RHisFaceDwon)
                {
                    if(RHcurrentPos.y - RHprevPos.y < 0)
                    {
                        RHamount = RHcurrentPos.y - RHprevPos.y;
                    }
                }

                if(LHisFaceUp)
                {
                    if(LHcurrentPos.y - LHprevPos.y > 0)
                    {
                        LHamount = LHcurrentPos.y - LHprevPos.y;
                    }
                }
                else if(LHisFaceDwon)
                {
                    if(LHcurrentPos.y - LHprevPos.y < 0)
                    {
                        LHamount = LHcurrentPos.y - LHprevPos.y;
                    }
                }
                OnRisingOrFalling(RHamount, LHamount);
            }
        }
    }
}
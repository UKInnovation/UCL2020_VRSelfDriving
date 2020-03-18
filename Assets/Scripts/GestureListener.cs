using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;
using UnityEngine.UI;

namespace YoyouOculusFramework
{
    public class GestureListener : MonoBehaviour
    {
        public static GestureListener INSTANCE;
        public Text text;
        public Hands Hands;
        private Hand LeftHand;
        private Hand RightHand;
        private Transform LeftHandT;
        private Transform RightHandT;

        private Queue<HandState> LeftHandStates;
        private Queue<HandState> RightHandStates;

        [SerializeField]
        private int QueueMaxLength;
        private float[] AveragePinchStrengthLeft;
        private float[] AveragePinchStrengthRight;

        public UnityEvent SlapRight;
        public UnityEvent OKgesture;
        private GestureEvent SlapRightG;
        private bool[] _hasGesture = new bool[]{false, false, false, false, false, false, false, false, false, false};
        public bool[] HasGesture{get{return _hasGesture;}}

        void Awake() 
        {
            INSTANCE = this;
        }
        void Start()
        {
            Hands = Hands.Instance;
            LeftHand = Hands.LeftHand;
            RightHand = Hands.RightHand;
            LeftHandT = LeftHand.gameObject.transform;
            RightHandT = RightHand.gameObject.transform;
            LeftHandStates = new Queue<HandState>();
            RightHandStates = new Queue<HandState>();
            AveragePinchStrengthLeft = AveragePinchStrengthRight = new float[5] {0f, 0f, 0f, 0f, 0f};
            SlapRightG = new GestureEvent(SlapRight);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateHandsStates();
            CalculateAveragePinch();
            UpdateCurrentGesture();
            InvokeGestureEvents();
            // text.text = RightHandT.rotation.eulerAngles.x.ToString() + " " + RightHandT.rotation.eulerAngles.y.ToString() + " " + RightHandT.rotation.eulerAngles.z.ToString();
        }

        private void UpdateHandsStates()
        {
            if(LeftHandStates.Count > QueueMaxLength)
            {
                LeftHandStates.Dequeue();
            }
            LeftHandStates.Enqueue(new HandState(LeftHandT.localPosition, LeftHandT.localRotation, LeftHand.State.PinchStrength));

            if(RightHandStates.Count > QueueMaxLength)
            {
                RightHandStates.Dequeue();
            }
            RightHandStates.Enqueue(new HandState(RightHandT.localPosition, RightHandT.localRotation, RightHand.State.PinchStrength));
        }

        private void CalculateAveragePinch()
        {
            AveragePinchStrengthLeft = AveragePinchStrengthRight = new float[5] {0f, 0f, 0f, 0f, 0f};
            HandState[] rightHandStates = RightHandStates.ToArray();
            int counter = 0;
            for (int i = 0; i < rightHandStates.Length; i += 5)
            {
                if(rightHandStates[i].fingerPinchStrengths != null){
                    for (int j = 0; j < rightHandStates[i].fingerPinchStrengths.Length; j++)
                    {
                        AveragePinchStrengthRight[j] = AveragePinchStrengthRight[j] + rightHandStates[i].fingerPinchStrengths[j];
                    }
                    counter++;
                }
            }
            if (counter > 0){
                for (int i = 0; i < 5; i++)
                {
                    AveragePinchStrengthRight[i] = AveragePinchStrengthRight[i] / counter;
                }
            }

            // HandState[] leftHandStates = LeftHandStates.ToArray();
            // counter = 0;
            // for (int i = 0; i < leftHandStates.Length; i = i + 5)
            // {
                
            //     if(leftHandStates[i].fingerPinchStrengths != null){
            //         for (int j = 0; j < leftHandStates[i].fingerPinchStrengths.Length; j++)
            //         {
            //             AveragePinchStrengthLeft[j] = AveragePinchStrengthLeft[j] + leftHandStates[i].fingerPinchStrengths[j];
            //         }
            //         counter++;
            //     }
            // }
            // if(counter > 0)
            // {
            //     for (int i = 0; i < 5; i++)
            //     {
            //         AveragePinchStrengthLeft[i] = AveragePinchStrengthLeft[i] / counter;
            //     }
            // }
        }

        private void InvokeGestureEvents()
        {
            InvokeSlapRight();
            InvokeOKgesture();
        }

        private void InvokeSlapRight(){
            if (SlapRight != null){
                HandState[] rightHandStates = RightHandStates.ToArray();
                if (AveragePinchStrengthRight[1] < 0.2){
                    Vector3 initialHandRotation = rightHandStates[0].rotation.eulerAngles;
                    Vector3 endHandRotation = rightHandStates[rightHandStates.Length - 1].rotation.eulerAngles;
                    Vector3 angleDiff = endHandRotation - initialHandRotation;
                    if((initialHandRotation.x > 0 && initialHandRotation.x < 100)
                    && (initialHandRotation.y > 0 && initialHandRotation.y < 100)
                    && (initialHandRotation.z > 260 && initialHandRotation.z < 320))
                    {
                        text.text = initialHandRotation.x.ToString() + " " + initialHandRotation.y.ToString() + " " + initialHandRotation.z.ToString();
                        if(angleDiff.x < 30 && endHandRotation.y > 270 && angleDiff.z > 50)
                        {
                            text.text = angleDiff.x.ToString() + " " + angleDiff.z.ToString();
                            SlapRightG.Invoke();
                            _hasGesture[(int)Gesture.RHSlapRight] = true;
                        }
                    }
                }
            }
        }

        private void InvokeOKgesture(){
            HandState[] rightHandStates = RightHandStates.ToArray();
            Vector3 AverageHandRogation = new Vector3();
            for(int i = 0; i < rightHandStates.Length; i += 5)
            {
                AverageHandRogation += rightHandStates[i].rotation.eulerAngles;
            }
            AverageHandRogation = AverageHandRogation / (QueueMaxLength/5);
            // text.text = AverageHandRogation.x.ToString() + " " + AverageHandRogation.y.ToString() + " " + AverageHandRogation.z.ToString();
            if (AveragePinchStrengthRight[1] == 1 && AveragePinchStrengthRight[2] == 0 && AveragePinchStrengthRight[3] == 0 && AveragePinchStrengthRight[4] == 0){
                if(AverageHandRogation.x > 340 && AverageHandRogation.y > 0 && AverageHandRogation.y < 40 && AverageHandRogation.z > 240 && AverageHandRogation.z < 280)
                {
                    OKgesture.Invoke();
                    _hasGesture[(int)Gesture.RHOK] = true;
                }
            }
        }

        private void UpdateCurrentGesture()
        {
            UpdateHandFaceUp();
        }

        private void UpdateHandFaceUp()
        {
            if(RHisFaceUP()){
                _hasGesture[(int)Gesture.RHFaceUp] = true;
            }
            else{
                _hasGesture[(int)Gesture.RHFaceUp] = false;
            }

            if(LHisFaceUP()){
                _hasGesture[(int)Gesture.LHFaceUp] = true;
            }
            else{
                _hasGesture[(int)Gesture.LHFaceUp] = false;
            }

            if(RHisFaceDown()){
                _hasGesture[(int)Gesture.RHFaceDown] = true;
            }
            else{
                _hasGesture[(int)Gesture.RHFaceDown] = false;
            }

            if(LHisFaceDown()){
                _hasGesture[(int)Gesture.LHFaceDown] = true;
            }
            else{
                _hasGesture[(int)Gesture.LHFaceDown] = false;
            }
            
        }

        private bool RHisFaceUP()
        {
            Quaternion HandRotation = RightHandT.rotation;
            Vector3 HandEulerAngle = HandRotation.eulerAngles;
            if((HandEulerAngle.x < 30 || HandEulerAngle.x > 320) 
                && HandEulerAngle.y < 300 && HandEulerAngle.y > 230
                && HandEulerAngle.z > 120 && HandEulerAngle.z < 240)
                {
                    return true;
                }
            return false;
        }

        private bool RHisFaceDown()
        {
            Quaternion HandRotation = RightHandT.rotation;
            Vector3 HandEulerAngle = HandRotation.eulerAngles;
            if((HandEulerAngle.x < 30 || HandEulerAngle.x > 320) 
                && HandEulerAngle.y < 120 && HandEulerAngle.y > 60
                && (HandEulerAngle.z > 330 || HandEulerAngle.z < 30))
                {
                    return true;
                }
            return false;
        }

        private bool LHisFaceUP()
        {
            Quaternion HandRotation = LeftHandT.rotation;
            Vector3 HandEulerAngle = HandRotation.eulerAngles;
            if((HandEulerAngle.x < 30 || HandEulerAngle.x > 320) 
                && HandEulerAngle.y < 300 && HandEulerAngle.y > 230
                && (HandEulerAngle.z < 30 || HandEulerAngle.z > 320))
                {
                    return true;
                }
            return false;
        }

        private bool LHisFaceDown()
        {
            Quaternion HandRotation = LeftHandT.rotation;
            Vector3 HandEulerAngle = HandRotation.eulerAngles;
            if((HandEulerAngle.x < 30 || HandEulerAngle.x > 320) 
                && HandEulerAngle.y < 130 && HandEulerAngle.y > 60
                && (HandEulerAngle.z > 140 && HandEulerAngle.z < 200))
                {
                    return true;
                }
            return false;
        }

        public class HandState{
            public readonly Vector3 position;
            public readonly Quaternion rotation;
            public readonly float[] fingerPinchStrengths;

            public HandState(Vector3 position, Quaternion rotation, float[] fingerPinchStrengths)
            {
                this.position = position;
                this.rotation = rotation;
                this.fingerPinchStrengths = fingerPinchStrengths;
            }
        }

        public class GestureEvent{
            private UnityEvent Event;
            private long timer;

            public GestureEvent(UnityEvent Event){
                this.Event = Event;
                timer = 0;
            }

            public void Invoke(){
            long TimeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - timer;
            if (TimeElapsed > 1200){
                Event.Invoke();
                timer = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
            }

        }

        public enum Gesture
        {
            RHSlapRight = 0,
            RHSlapLeft = 1,
            RHOK = 2,
            RHFaceUp = 3,
            RHFaceDown = 4,
            LHSlapRight = 5,
            LHSlapLeft = 6,
            LHOK = 7,
            LHFaceUp = 8,
            LHFaceDown = 9
        }
    }
}
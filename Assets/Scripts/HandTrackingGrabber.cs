using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

namespace YoyouOculusFramework
{
    public class HandTrackingGrabber : OVRGrabber
    {
        [SerializeField]
        private bool FixXAxis;
        [SerializeField]
        private bool FixYAxis;
        [SerializeField]
        private bool FixZAxis;
        [SerializeField]
        private bool FixRotation;
        private Hand hand;
        private float pinchThreshold = 0.9f;
        protected override void Start()
        {
            base.Start();
            hand = GetComponent<Hand>();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            CheckIndexPinch();
            CheckOutOfBound();
        }

        void CheckIndexPinch()
        {
            float pinchStrength = hand.PinchStrength(OVRPlugin.HandFinger.Index);
            bool isPinching = pinchStrength > pinchThreshold;

            if(!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
            {
                GrabBegin();
            }
            else if(m_grabbedObj && !isPinching)
            {
                GrabEnd();
            }
        }

        void CheckOutOfBound()
        {
            // if(m_grabbedObj.gameObject.GetComponent<LimitedGrabbable>() != null)
            // {
            //     if(m_grabbedObj.transform.localPosition.x > m_grabbedObj.gameObject.GetComponent<LimitedGrabbable>().local_X_position_Max
            //             || m_grabbedObj.transform.localPosition.x < m_grabbedObj.gameObject.GetComponent<LimitedGrabbable>().local_X_position_Min)
            //     {
            //         GrabEnd();
            //     }
            // }
        }

        protected override void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
        {
            if (m_grabbedObj == null)
            {
                return;
            }

            Rigidbody grabbedRigidbody = m_grabbedObj.grabbedRigidbody;
            Vector3 grabbablePosition = pos + rot * m_grabbedObjectPosOff;
            Quaternion grabbableRotation = rot * m_grabbedObjectRotOff;

            Vector3 pos_diff = grabbablePosition - grabbedRigidbody.transform.position;
            

            if(grabbedRigidbody.gameObject.name == "Progress Controller")
            {
                grabbedRigidbody.transform.position += Vector3.ProjectOnPlane(Vector3.ProjectOnPlane(pos_diff, grabbedRigidbody.transform.forward), grabbedRigidbody.transform.up); 
            }
            else
            {
                grabbedRigidbody.transform.position = grabbablePosition;
                grabbedRigidbody.transform.rotation = grabbableRotation;
            }
        }
    }

}
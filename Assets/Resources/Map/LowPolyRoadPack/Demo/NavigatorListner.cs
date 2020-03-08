using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class NavigatorListner : MonoBehaviour
    {
        private Queue<Rail> rails;
        private Rail currentRail;
        private float _nextWheelAngle;
        private float _nextTorque;

        public float NextWheelAngle {get{return _nextWheelAngle;}}
        public float NextTorque {get{return _nextTorque;}}
        public Rail CurrentRail{get {return currentRail;}}

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            rails = new Queue<Rail>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            UpdateWheelAngle();
            UpdateTorque();
            // DequeDisabledRail();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<Rail>()!= null)
            {
                rails.Enqueue(other.gameObject.GetComponent<Rail>());
                currentRail = other.gameObject.GetComponent<Rail>();
                Debug.Log(other.gameObject.name);
            }   
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.gameObject.GetComponent<Rail>() != null)
            {
                rails.Dequeue();
                if(rails.Count == 0)
                {
                    currentRail = null;
                }
            }
        }

        private void UpdateWheelAngle()
        {
            if(currentRail != null)
            {
                if(currentRail.isActive)
                {
                    _nextWheelAngle = currentRail.Direction.y - transform.rotation.eulerAngles.y;
                    if(_nextWheelAngle > 180)
                    {
                        _nextWheelAngle -= 360;
                    }
                    else if(_nextWheelAngle < -180)
                    {
                        _nextWheelAngle += 360;
                    }
                }
            }
            else
            {
                _nextWheelAngle = 0;
            }
        }

        private void UpdateTorque()
        {
            if(currentRail != null)
            {
                if(currentRail.isActive)
                {
                    _nextTorque = 1;
                }
                else{
                    _nextTorque = 0;
                }
            }
            else
            {
                _nextTorque = 0;
            }
        }

        // private void DequeDisabledRail()
        // {
        //     if(rails.Count > 0)
        //     {
        //         if(rails.Peek().Railway.enabled == false)
        //         {
        //             rails.Dequeue();
        //             if(rails.Count == 0)
        //             {
        //                 currentRail = null;
        //             }
        //         }
        //     }
        // }
    }
}

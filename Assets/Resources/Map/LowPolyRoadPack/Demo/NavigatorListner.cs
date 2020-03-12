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
        private float _nextBrakeTorqueRatio;
        private bool _adjustPositionToRoadCenter = false;

        public float NextWheelAngle {get{return _nextWheelAngle;}}
        public float NextTorque {get{return _nextTorque;}}
        public float NextBrakeToruqeRatio {get{return _nextBrakeTorqueRatio;}}
        public Rail CurrentRail{get {return currentRail;}}
        public bool ArrivedDestination = false;
        public Rigidbody carR;

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
            UpdateBrakeTorque();
            UpdateRailCompletePercentage();
            Debug.Log(_nextBrakeTorqueRatio);
            // DequeDisabledRail();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<Rail>()!= null)
            {
                rails.Enqueue(other.gameObject.GetComponent<Rail>());
                currentRail = other.gameObject.GetComponent<Rail>();
            }   
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.gameObject.GetComponent<Rail>() != null)
            {
                Rail rail = rails.Dequeue();
                if(rail.gameObject.GetComponent<Edge>() != null)
                {
                    rail.gameObject.GetComponent<Edge>().DeActivate();    
                }
                else
                {
                    rail.DeActivate();
                }

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
                    if(_adjustPositionToRoadCenter)
                    {
                        if(Vector3.Project(transform.position - currentRail.transform.position, currentRail.transform.right).magnitude < 0.5)
                        {
                            _adjustPositionToRoadCenter = false;
                            return;
                        }
                        if(Vector3.Dot(transform.position - currentRail.transform.position, currentRail.transform.right) > 0)
                        {
                            _nextWheelAngle = -10;
                        }
                        else
                        {
                            _nextWheelAngle = 10;
                        }
                        return;
                    }

                    float angleDiff;
                    angleDiff = currentRail.DirectionAngle.y - transform.rotation.eulerAngles.y;
                    if(angleDiff > 180)
                    {
                        angleDiff -= 360;
                    }
                    else if(angleDiff < -180)
                    {
                        angleDiff += 360;
                    }
                    _nextWheelAngle = angleDiff;
                    if(angleDiff < 0.1)
                    {
                        _adjustPositionToRoadCenter = true;
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
                    if(carR.velocity.magnitude < 3)
                    {
                        _nextTorque = 1;
                    }
                    else
                    {
                        _nextTorque = 0;
                    }
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

        private void UpdateBrakeTorque()
        {
            if(currentRail == null || !currentRail.isActive)
            {
                if(_nextBrakeTorqueRatio < 1)
                {
                    _nextBrakeTorqueRatio += 0.1f;
                }
            }
            else
            {
                _nextBrakeTorqueRatio = 0;
            }
        }

        private void UpdateRailCompletePercentage()
        {
            if(currentRail != null)
            {
                Vector3 RailCentertoCarV = transform.position - currentRail.transform.position;
                float disFromRailCenterToCar;
                if(Vector3.Dot(RailCentertoCarV, currentRail.DirectionVector) > 0)
                {
                    disFromRailCenterToCar = Vector3.Magnitude(Vector3.Project(RailCentertoCarV, currentRail.DirectionVector));
                }
                else
                {
                    disFromRailCenterToCar = -Vector3.Magnitude(Vector3.Project(RailCentertoCarV, currentRail.DirectionVector));
                }

                float disFromCarToRailStart = disFromRailCenterToCar + currentRail.Distance/2;
                currentRail.RailCompletePercentage = disFromCarToRailStart / currentRail.Distance;
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

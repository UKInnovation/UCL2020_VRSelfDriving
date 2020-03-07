using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class NavigatorListner : MonoBehaviour
    {
        private Rail currentRail;
        private float _angelDiff;
        public float AngleDiff {get{return _angelDiff;}}

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<Rail>()!= null)
            {
                currentRail = other.gameObject.GetComponent<Rail>();
            }     
        }

        private void UpdateWheelAngel()
        {
            _angelDiff = currentRail.Direction.y - transform.rotation.eulerAngles.y;
            if(_angelDiff > 180)
            {
                _angelDiff -= 360;
            }
            else if(_angelDiff < -180)
            {
                _angelDiff += 360;
            }
        }
    }
}

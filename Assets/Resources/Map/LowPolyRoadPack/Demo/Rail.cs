using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace VehicleNavigation
{
    public class Rail : MonoBehaviour
    {
        private BoxCollider _railway;
        private Vector3 _directionAngle;
        private Vector3 _directionVector;
        private bool _isActive;
        private float _railCompletePercentage;
        private float _distance;
        private Image _routeDisplayer;

        public BoxCollider Railway{get{return _railway;}}
        public Vector3 DirectionAngle {get{return _directionAngle;}}
        public Vector3 DirectionVector {get{return _directionVector;}}
        public bool isActive{get{return _isActive;}}
        public float RailCompletePercentage {get{return _railCompletePercentage;} set{_railCompletePercentage = value;}}
        public float Distance {get{return _distance;}}

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Assert.IsNotNull<BoxCollider>(this.gameObject.GetComponent<BoxCollider>());

            this._directionAngle = this.gameObject.transform.rotation.eulerAngles;
            this._directionVector = this.gameObject.transform.forward;
            this._distance =  transform.localScale.z;
            this._railway = this.gameObject.GetComponent<BoxCollider>();
            _routeDisplayer = this.GetComponentInChildren<Image>();

            _railway.isTrigger = true;
            _railway.enabled = true;
            _isActive = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            // DeActivate();
        }

        // void Start() {
        //     DeActivate();
        // }

        public void Activate()
        {
            // _railway.enabled = true;
            _isActive = true;
            _routeDisplayer.enabled = true;
        }

        public void DeActivate()
        {
            // _railway.enabled = false;
            _isActive = false;
            _routeDisplayer.enabled = false;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            _routeDisplayer.fillAmount = (1 - _railCompletePercentage);
        }
    }
}
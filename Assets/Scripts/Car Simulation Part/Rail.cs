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
        private SpriteRenderer _routeDisplayer;

        public BoxCollider Railway { get { return _railway; } }
        public Vector3 DirectionAngle { get { return _directionAngle; } }
        public Vector3 DirectionVector { get { return _directionVector; } }
        public bool isActive { get { return _isActive; } }
        public float RailCompletePercentage { get { return _railCompletePercentage; } set { _railCompletePercentage = value; } }
        public float Distance { get { return _distance; } }

        void Awake()
        {
            Assert.IsNotNull<BoxCollider>(this.gameObject.GetComponent<BoxCollider>());

            this._directionAngle = this.gameObject.transform.rotation.eulerAngles;
            this._directionVector = this.gameObject.transform.forward;
            this._distance = transform.localScale.z;
            this._railway = this.gameObject.GetComponent<BoxCollider>();
            _routeDisplayer = this.GetComponentInChildren<SpriteRenderer>();

            _railway.isTrigger = true;
            _railway.enabled = true;
            _isActive = false;
            _routeDisplayer.enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        public void Activate()
        {
            _isActive = true;
            _routeDisplayer.enabled = true;
        }

        public void DeActivate()
        {
            _isActive = false;
            _routeDisplayer.enabled = false;
        }

        void Update()
        {
            _routeDisplayer.size = new Vector2((1 - _railCompletePercentage), 1);
            _routeDisplayer.transform.localPosition = new Vector3(_routeDisplayer.transform.localPosition.x, _routeDisplayer.transform.localPosition.y, 0.5f - ((1 - _railCompletePercentage) / 2));
        }

        public void DisableRouteDisplayer()
        {
            _routeDisplayer.enabled = false;
        }
    }
}
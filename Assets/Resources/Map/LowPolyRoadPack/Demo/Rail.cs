using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VehicleNavigation
{
    public class Rail : MonoBehaviour
    {
        private BoxCollider _railway;
        private Vector3 _direction;
        private bool _isActive;
        public BoxCollider Railway{get{return _railway;}}
        public Vector3 Direction {get{return _direction;}}
        public bool isActive{get{return _isActive;}}

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Assert.IsNotNull<BoxCollider>(this.gameObject.GetComponent<BoxCollider>());

            this._direction = this.gameObject.transform.rotation.eulerAngles;
            this._railway = this.gameObject.GetComponent<BoxCollider>();

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
        }

        public void DeActivate()
        {
            // _railway.enabled = false;
            _isActive = false;
        }
    }
}
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
        public Vector3 Direction {get{return _direction;}}

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Assert.IsNotNull<BoxCollider>(this.gameObject.GetComponent<BoxCollider>());

            this._direction = this.gameObject.transform.rotation.eulerAngles;
            this._railway = this.gameObject.GetComponent<BoxCollider>();

            _railway.isTrigger = true;
            DeActivate();
        }

        public void Activate()
        {
            _railway.enabled = true;
        }

        public void DeActivate()
        {
            _railway.enabled = false;
        }
    }
}
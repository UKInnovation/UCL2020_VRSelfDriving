using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class CheckPoint : MonoBehaviour
    {
        private Edge _parentEdge;
        private Vector3 _position;

        public Edge ParentEdge {get{return _parentEdge;}}
        public Vector3 Position {get{return _position;}}
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _parentEdge = transform.parent.GetComponent<Edge>();
            _position = transform.position;
        }
    }
}
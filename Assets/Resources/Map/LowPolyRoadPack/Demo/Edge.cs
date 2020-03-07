using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
     public class Edge : MonoBehaviour
    {
        private Vector3 _direction;
        [SerializeField]
        private Vertex _startVertex;
        [SerializeField]
        private Vertex _endVertex;
        private float _distance;
        private Rail _rail;

        public Vector3 Direction{get{return _direction;}}
        public Vertex StartVertex{get{return _startVertex;}}
        public Vertex EndVertex{get{return _endVertex;}}
        public float Distance{get{return _distance;}}
        public bool isActive = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _rail = this.GetComponent<Rail>();    
        }

        public void Activate()
        {
            _rail.Activate();
            isActive = true;
        }

        public void DeActivate()
        {
            _rail.DeActivate();
            isActive = false;
        }
    }   
}

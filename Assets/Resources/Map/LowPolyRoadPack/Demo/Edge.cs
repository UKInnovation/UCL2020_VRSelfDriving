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
        // private List<CheckPoint> _checkPoints;

        public Vector3 Direction{get{return _direction;}}
        public Vertex StartVertex{get{return _startVertex;}}
        public Vertex EndVertex{get{return _endVertex;}}
        public float Distance{get{return _distance;}}
        // public List<CheckPoint> CheckPoints{get{return _checkPoints;}}
        public bool isActive = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _distance = transform.localScale.z;
            _direction = transform.forward;
            _rail = this.GetComponent<Rail>();
            // _checkPoints = new List<CheckPoint>(this.GetComponentsInChildren<CheckPoint>());
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

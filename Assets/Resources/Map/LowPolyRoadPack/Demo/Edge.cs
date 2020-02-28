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

        public Vector3 Direction{get{return _direction;}}
        public Vertex StartVertex{get{return _startVertex;}}
        public Vertex EndVertex{get{return _endVertex;}}
        public float Distance{get{return _distance;}}

        public void setActive()
        {
            Destroy(GetComponent<MeshRenderer>());
        }
    }   
}

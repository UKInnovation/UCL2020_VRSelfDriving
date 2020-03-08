using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class Vertex : MonoBehaviour
    {
        [SerializeField]
        private List<Edge> _outGoingEdges;
        private List<Vertex> _reachableVertexs;
        private Vertex _prev_Ver;
        private Edge _prev_Edge;

        public List<Edge> OutGoingEdge{get {return _outGoingEdges;}}
        public List<Vertex> ReachableVertexs{get {return _reachableVertexs;}}
        public Vertex Prev_Ver{get{return _prev_Ver;} set{_prev_Ver = value;}}
        public Edge Prev_Edge{get{return _prev_Edge;} set{_prev_Edge = value;}}
        
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _outGoingEdges = new List<Edge>();
        }
        
        public void addEdge(Edge edge)
        {
            _outGoingEdges.Add(edge);
        }

        public void setActive()
        {
            Destroy(GetComponent<MeshRenderer>());  
        }
    }
}

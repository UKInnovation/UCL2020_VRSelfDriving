using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleNavigation
{
    public class Navigator : MonoBehaviour
    {
        private Vertex[] _vertexs;
        private Edge[] _edges;

        public Vertex[] Vertices {get{return _vertexs;}}
        public Edge[] Edges {get{return _edges;}}

        public Vertex StartPoint;
        public Vertex EndPoint;

        public Text text;
        private List<Vertex> currentActivedVertices = new List<Vertex>();
        private List<Edge> currentActivedEdges = new List<Edge>();
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _vertexs = GetComponentsInChildren<Vertex>();
            _edges = GetComponentsInChildren<Edge>();
            for(int i = 0; i < _edges.Length; i++)
            {
                _edges[i].StartVertex.addEdge(_edges[i]);

            }
        }

        public void getShortestRoute(Vertex StartPoint, Vertex Destnation) 
        {
            Queue<Vertex> Vers = new Queue<Vertex>();
            List<Vertex> Visited_Vers = new List<Vertex>();
            Vers.Enqueue(StartPoint);
            Visited_Vers.Add(StartPoint);
            // text.text = StartPoint.OutGoingEdge.Count.ToString();
            while(true)
            {
                Vertex current_Ver = Vers.Dequeue();
                if(current_Ver == Destnation)
                {
                    break;
                }
                for(int i = 0; i < current_Ver.OutGoingEdge.Count; i++)
                {
                    Vertex reachableVertex = current_Ver.OutGoingEdge[i].EndVertex;
                    if(!Visited_Vers.Contains(reachableVertex))
                    {
                        reachableVertex.Prev_Ver = current_Ver;
                        reachableVertex.Prev_Edge = current_Ver.OutGoingEdge[i];
                        Vers.Enqueue(reachableVertex);
                        Visited_Vers.Add(reachableVertex);
                    }
                }
            }

            // List<Vertex> shortestRoute = new List<Vertex>();
            Vertex vertex = Destnation;
            while(vertex != null)
            {
                vertex.setActive();
                currentActivedVertices.Add(vertex);
                if(vertex.Prev_Edge != null) 
                {
                    vertex.Prev_Edge.Activate();
                    currentActivedEdges.Add(vertex.Prev_Edge);
                }
                vertex = vertex.Prev_Ver;
            }
        }

        public void DeactivateActivedNavigatorElements()
        {
            foreach(Vertex vertex in currentActivedVertices)
            {
                vertex.DeActivate();
            }
            foreach(Edge edge in currentActivedEdges)
            {
                edge.DeActivate();
            }
        }
    }
}
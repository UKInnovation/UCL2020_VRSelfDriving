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
        void Start()
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
            List<Vertex> Vers = new List<Vertex>();
            List<Vertex> Visited_Vers = new List<Vertex>();
            Dictionary<Vertex, float> Hash = new Dictionary<Vertex, float>();

            Vers.Add(StartPoint);
            Visited_Vers.Add(StartPoint);
            Hash[StartPoint] = 0;
            // text.text = StartPoint.OutGoingEdge.Count.ToString();
            while(Vers.Count != 0)
            {
                Vertex current_Ver = null;
                float shortestDistance = int.MaxValue;
                foreach(Vertex ver in Vers)
                {
                    if(Hash[ver] < shortestDistance)
                    {
                        shortestDistance = Hash[ver];
                        current_Ver = ver;
                    }
                }
                Vers.Remove(current_Ver);

                if(current_Ver == Destnation)
                {
                    break;
                }

                for(int i = 0; i < current_Ver.OutGoingEdge.Count; i++)
                {
                    float AngleDiff = 0;
                    if(current_Ver.Prev_Edge != null)
                    {
                        AngleDiff = Mathf.Abs(current_Ver.OutGoingEdge[i].transform.eulerAngles.y - current_Ver.Prev_Edge.transform.eulerAngles.y);
                        if(AngleDiff > 180)
                        {
                            AngleDiff = 360 - AngleDiff;
                        }
                    }
                    // Debug.Log(current_Ver.OutGoingEdge[i].gameObject.name);
                    // Debug.Log(current_Ver.OutGoingEdge[i].transform.eulerAngles.y);
                    // Debug.Log(current_Ver.Prev_Edge.gameObject.name);
                    // Debug.Log(current_Ver.Prev_Edge.transform.eulerAngles.y);
                    // Debug.Log(AngleDiff);
                

                    if(AngleDiff < 100)
                    {
                        Vertex reachableVertex = current_Ver.OutGoingEdge[i].EndVertex;
                        if(!Visited_Vers.Contains(reachableVertex))
                        {
                            reachableVertex.Prev_Ver = current_Ver;
                            reachableVertex.Prev_Edge = current_Ver.OutGoingEdge[i];
                            Hash[reachableVertex] = Hash[current_Ver] + current_Ver.OutGoingEdge[i].Distance;
                            Vers.Add(reachableVertex);
                            Visited_Vers.Add(reachableVertex);
                        }
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

        public void ActiveEdge(Edge edge)
        {
            currentActivedEdges.Add(edge);
            edge.Activate();
        }
    }
}
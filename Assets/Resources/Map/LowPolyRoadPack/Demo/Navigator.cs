using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class Navigator : MonoBehaviour
    {
        private Vertex[] vertexs;
        Edge[] edges;

        public Vertex StartPoint;
        public Vertex EndPoint;
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            vertexs = GetComponentsInChildren<Vertex>();
            edges = GetComponentsInChildren<Edge>();
            for(int i = 0; i < edges.Length; i++)
            {
                edges[i].StartVertex.addEdge(edges[i]);
            }
        }

        void Start()
        {
            // getShortestRoute(StartPoint, EndPoint);    
        }

        public List<Vertex> getShortestRoute(Vertex StartPoint, Vertex Destnation) 
        {
            Queue<Vertex> Vers = new Queue<Vertex>();
            List<Vertex> Visited_Vers = new List<Vertex>();
            Vers.Enqueue(StartPoint);
            Visited_Vers.Add(StartPoint);

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

            List<Vertex> shortestRoute = new List<Vertex>();
            Vertex vertex = Destnation;
            while(vertex != null)
            {
                vertex.setActive();
                if(vertex.Prev_Edge != null) vertex.Prev_Edge.Activate();
                vertex = vertex.Prev_Ver;
            }
            shortestRoute.Reverse();
            return shortestRoute;
        }
    }
}
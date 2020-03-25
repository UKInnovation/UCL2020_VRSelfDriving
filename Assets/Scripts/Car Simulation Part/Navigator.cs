using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace VehicleNavigation
{
    public class Navigator : MonoBehaviour
    {
        private Vertex[] _vertexs;
        private Edge[] _edges;

        public Vertex[] Vertices { get { return _vertexs; } }
        public Edge[] Edges { get { return _edges; } }

        public Vertex StartPoint;
        public Vertex EndPoint;

        private List<Vertex> currentActivedVertices = new List<Vertex>();
        private List<Edge> currentActivedEdges = new List<Edge>();
        public UnityEvent OnNavigationElementsChange;
        private Text text;

        void Start()
        {
            _vertexs = GetComponentsInChildren<Vertex>();
            _edges = GetComponentsInChildren<Edge>();
            for (int i = 0; i < _edges.Length; i++)
            {
                _edges[i].StartVertex.addEdge(_edges[i]);
            }
            text = GameObject.Find("Text").GetComponent<Text>();
        }

        public void ActivateShortestRoute(Vertex StartPoint, Vertex Destination, Edge StartEdge)
        {
            List<Vertex> Vers = new List<Vertex>();
            List<Vertex> Visited_Vers = new List<Vertex>();
            Dictionary<Vertex, float> Hash = new Dictionary<Vertex, float>();

            StartPoint.Prev_Edge = StartEdge;
            Vers.Add(StartPoint);
            Visited_Vers.Add(StartPoint);

            Hash[StartPoint] = 0;
            int counter = 0;
            while (Vers.Count != 0)
            {
                text.text = counter.ToString();

                counter++;
                Vertex current_Ver = null;
                float shortestDistance = int.MaxValue;
                foreach (Vertex ver in Vers)
                {
                    if (Hash[ver] < shortestDistance)
                    {
                        shortestDistance = Hash[ver];
                        current_Ver = ver;
                    }
                }

                Vers.Remove(current_Ver);

                if (current_Ver == Destination)
                {
                    break;
                }

                for (int i = 0; i < current_Ver.OutGoingEdge.Count; i++)
                {
                    float AngleDiff = 0;
                    if (current_Ver.Prev_Edge != null)
                    {
                        AngleDiff = Mathf.Abs(current_Ver.OutGoingEdge[i].transform.eulerAngles.y - current_Ver.Prev_Edge.transform.eulerAngles.y);
                        if (AngleDiff > 180)
                        {
                            AngleDiff = 360 - AngleDiff;
                        }
                    }

                    if (AngleDiff < 100)
                    {
                        Vertex reachableVertex = current_Ver.OutGoingEdge[i].EndVertex;
                        if (!Visited_Vers.Contains(reachableVertex))
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
            Vertex vertex = Destination;
            while (vertex != null)
            {
                vertex.setActive();
                currentActivedVertices.Add(vertex);
                if (vertex.Prev_Edge != null)
                {
                    vertex.Prev_Edge.Activate();
                    currentActivedEdges.Add(vertex.Prev_Edge);
                }
                vertex = vertex.Prev_Ver;
            }
            if (OnNavigationElementsChange != null)
            {
                OnNavigationElementsChange.Invoke();
            }
        }

        public void DeactivateActivedNavigatorElements()
        {
            foreach (Vertex vertex in _vertexs)
            {
                vertex.DeActivate();
                vertex.reset();
            }
            foreach (Edge edge in _edges)
            {
                edge.DeActivate();
            }
            if (OnNavigationElementsChange != null)
            {
                OnNavigationElementsChange.Invoke();
            }
        }

        public void ActiveEdge(Edge edge)
        {
            currentActivedEdges.Add(edge);
            edge.Activate();
        }
    }
}
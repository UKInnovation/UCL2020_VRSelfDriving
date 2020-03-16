using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VehicleNavigation
{
    public class NavigatorController : MonoBehaviour
    {
        [SerializeField]
        private Navigator navigator;
        [SerializeField]
        private NavigatorListner navigatorListner;
        public Vector3 destination;
        // private List<CheckPoint> checkPoints;


        // Update is called once per frame
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // checkPoints = new List<CheckPoint>();
            // foreach(Edge edge in navigator.Edges)
            // {
            //     foreach(CheckPoint checkPoint in edge.CheckPoints)
            //     {
            //         checkPoints.Add(checkPoint);
            //     }
            // }
            // StartCoroutine(test());
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(destination != Vector3.zero)
            {
                if(Vector3.Distance(transform.position, destination) < 5)
                {
                    navigator.DeactivateActivedNavigatorElements();
                    destination = Vector3.zero;
                    // NavigateTo(new Vector3(0, 0, 50));
                }
            }
        }

        public void directTo(Vertex vertex)
        {
            Vertex currentEdgeStartVertex;

            if(navigatorListner.CurrentRail.GetComponent<Edge>() != null)
            {
                currentEdgeStartVertex = navigatorListner.CurrentRail.GetComponent<Edge>().StartVertex;
            }
            else
            {
                currentEdgeStartVertex = navigatorListner.CurrentRail.transform.parent.GetComponent<Curve>().FromEdge.StartVertex;
            }
            if(currentEdgeStartVertex != vertex)
            {
                Debug.Log(vertex.gameObject.name);
                navigator.getShortestRoute(currentEdgeStartVertex, vertex);
            }
        }

        public void NavigateTo(Vector3 realWorldPos)
        {
            float shortestDistance = float.MaxValue;
            Edge closestEdge = null;
            foreach(Edge edge in navigator.Edges)
            {
                float Distance = Vector3.Distance(edge.transform.position, realWorldPos); 
                if(Distance < shortestDistance)
                {
                    shortestDistance = Distance;
                    closestEdge = edge;
                }
            }

            Vector3 FromEdgeToPos = realWorldPos - closestEdge.transform.position;
            Vector3 Offset = Vector3.Project(FromEdgeToPos, closestEdge.Direction);
            if(Offset.magnitude > closestEdge.Distance/2)
            {
                Offset = Offset.normalized * closestEdge.Distance/2;
            }

            destination = closestEdge.transform.position + Offset;
            if(Vector3.Dot(destination - transform.position, closestEdge.Direction) < 0 && closestEdge == navigatorListner.CurrentRail.GetComponent<Edge>())
            {
                destination = Vector3.zero;
            }
            else
            {
                directTo(closestEdge.EndVertex);
            }
        }

        // public void NavigateTo(CheckPoint checkPoint)
        // {
        //     destination = checkPoint;
        //     directTo(checkPoint.ParentEdge.EndVertex);
        // }
        // IEnumerator test()
        // {
        //     while(navigatorListner.CurrentRail == null) yield return null;
        //     directTo(destination);
        // }

        // IEnumerator test()
        // {
        //     while(navigatorListner.CurrentRail == null) yield return null;
        //     NavigateTo(new Vector3(200,0,200));
        // }
    }
}

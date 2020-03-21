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
            if (destination != Vector3.zero)
            {
                if (Vector3.Distance(transform.position, destination) < 5)
                {
                    navigator.DeactivateActivedNavigatorElements();
                    destination = Vector3.zero;
                    // NavigateTo(new Vector3(73, 0, -35));
                }
            }
        }

        public void directTo(Edge destinationEdge)
        {
            // Vertex StartVertex;

            // if(navigatorListner.CurrentRail.GetComponent<Edge>() != null)
            // {
            //     StartVertex = navigatorListner.CurrentRail.GetComponent<Edge>().EndVertex;
            //     if(StartVertex != vertex)
            //     {
            //         navigator.ActiveEdge(navigatorListner.CurrentRail.GetComponent<Edge>());
            //         StartVertex.Prev_Edge = navigatorListner.CurrentRail.GetComponent<Edge>();
            //     }
            //     else
            //     {
            //         StartVertex = navigatorListner.CurrentRail.GetComponent<Edge>().StartVertex;
            //     }
            // }
            // else
            // {
            //     StartVertex = navigatorListner.CurrentRail.transform.parent.GetComponent<Curve>().FromEdge.EndVertex;
            //     navigator.ActiveEdge(navigatorListner.CurrentRail.transform.parent.GetComponent<Curve>().FromEdge);
            // }
            // if(StartVertex != vertex)
            // {
            //     Debug.Log(StartVertex);
            //     Debug.Log(vertex);
            //     navigator.getShortestRoute(StartVertex, vertex);
            // }
            if (navigatorListner.CurrentRail.GetComponent<Edge>() != null)
            {
                Edge currentEdge = navigatorListner.CurrentRail.GetComponent<Edge>();
                if (currentEdge == destinationEdge)
                {
                    navigator.ActiveEdge(currentEdge);
                }
                else
                {
                    navigator.ActiveEdge(currentEdge);
                    navigator.ActiveEdge(destinationEdge);
                    if (currentEdge.EndVertex != destinationEdge.StartVertex)
                    {
                        navigator.getShortestRoute(currentEdge.EndVertex, destinationEdge.StartVertex, currentEdge, destinationEdge);
                    }
                }
            }
            else
            {
                Curve currentCurve = navigatorListner.CurrentRail.transform.parent.GetComponent<Curve>();
                Edge EdgeBeforeCurve = currentCurve.FromEdge;
                Edge EdgeAfterCurve = currentCurve.ToEdge;

                navigator.ActiveEdge(EdgeBeforeCurve);
                if (EdgeAfterCurve == destinationEdge)
                {
                    navigator.ActiveEdge(EdgeAfterCurve);
                }
                else
                {
                    navigator.ActiveEdge(EdgeAfterCurve);
                    navigator.ActiveEdge(destinationEdge);
                    if (EdgeAfterCurve.EndVertex != destinationEdge.StartVertex)
                    {
                        navigator.getShortestRoute(EdgeAfterCurve.EndVertex, destinationEdge.StartVertex, EdgeAfterCurve, destinationEdge);
                    }

                }


            }
        }

        public void NavigateTo(Vector3 realWorldPos)
        {
            float shortestDistance = float.MaxValue;
            Edge closestEdge = null;
            foreach (Edge edge in navigator.Edges)
            {
                float Distance = Vector3.Distance(edge.transform.position, realWorldPos);
                if (Distance < shortestDistance)
                {
                    shortestDistance = Distance;
                    closestEdge = edge;
                }
            }
            Debug.Log(closestEdge.gameObject.name);

            Vector3 FromEdgeToPos = realWorldPos - closestEdge.transform.position;
            Vector3 Offset = Vector3.Project(FromEdgeToPos, closestEdge.Direction);
            if (Offset.magnitude > closestEdge.Distance / 2)
            {
                Offset = Offset.normalized * closestEdge.Distance / 2;
            }

            destination = closestEdge.transform.position + Offset;
            // Debug.Log(closestEdge.gameObject.name);
            // Debug.Log(destination);
            if (Vector3.Dot(destination - transform.position, closestEdge.Direction) < 0 && closestEdge == navigatorListner.CurrentRail.GetComponent<Edge>())
            {
                destination = Vector3.zero;
            }
            else
            {
                directTo(closestEdge);
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

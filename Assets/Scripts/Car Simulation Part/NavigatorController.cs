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


        void Update()
        {
            if (destination != Vector3.zero)
            {
                if (Vector3.Distance(transform.position, destination) < 20)
                {
                    Debug.Log("arrived Des");
                    navigator.DeactivateActivedNavigatorElements();
                    destination = Vector3.zero;
                    // NavigateTo(new Vector3(73, 0, -35));
                }
            }
        }

        public void directTo(Edge destinationEdge)
        {
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
                    navigator.ActivateShortestRoute(currentEdge.EndVertex, destinationEdge.EndVertex, currentEdge);
                }
            }
            else
            {
                Curve currentCurve = navigatorListner.CurrentRail.transform.parent.GetComponent<Curve>();
                Edge EdgeBeforeCurve = currentCurve.FromEdge;
                Edge EdgeAfterCurve = currentCurve.ToEdge;

                navigator.ActiveEdge(EdgeBeforeCurve);
                EdgeBeforeCurve.DisableRouteDisplayer();
                if (EdgeAfterCurve == destinationEdge)
                {
                    navigator.ActiveEdge(EdgeAfterCurve);
                }
                else
                {
                    navigator.ActivateShortestRoute(EdgeAfterCurve.StartVertex, destinationEdge.EndVertex, EdgeAfterCurve);
                }
            }
        }

        public void NavigateTo(Vector3 realWorldPos)
        {
            navigator.DeactivateActivedNavigatorElements();
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
            Debug.Log(closestEdge.gameObject.name);
            Debug.Log(destination);

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

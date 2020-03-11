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
        public CheckPoint destination;
        private List<CheckPoint> checkPoints;


        // Update is called once per frame
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            checkPoints = new List<CheckPoint>();
            foreach(Edge edge in navigator.Edges)
            {
                foreach(CheckPoint checkPoint in edge.CheckPoints)
                {
                    checkPoints.Add(checkPoint);
                }
            }
            // StartCoroutine(test());
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(destination != null)
            {
                if(Vector3.Distance(transform.position, destination.Position) < 5)
                {
                    navigator.DeactivateActivedNavigatorElements();
                    destination = null;
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
                navigator.getShortestRoute(currentEdgeStartVertex, vertex);
            }
        }

        public void NavigateTo(Vector3 realWorldPos)
        {
            float shortestDistance = float.MaxValue;
            CheckPoint closestCheckPoint = null;
            foreach(CheckPoint checkPoint in checkPoints)
            {
                float Distance = Vector3.Distance(checkPoint.Position, realWorldPos); 
                if(Distance < shortestDistance)
                {
                    shortestDistance = Distance;
                    closestCheckPoint = checkPoint;
                }
            }
            Assert.IsNotNull(closestCheckPoint);
            NavigateTo(closestCheckPoint);
        }

        public void NavigateTo(CheckPoint checkPoint)
        {
            destination = checkPoint;
            directTo(checkPoint.ParentEdge.EndVertex);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class NavigatorController : MonoBehaviour
    {
        [SerializeField]
        private Navigator navigator;
        [SerializeField]
        private NavigatorListner navigatorListner;
        public Vertex destination;

        // Update is called once per frame
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            StartCoroutine(test());
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
            navigator.getShortestRoute(currentEdgeStartVertex, vertex);
        }

        IEnumerator test()
        {
            while(navigatorListner.CurrentRail == null) yield return null;
            directTo(destination);
        }
    }
}

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
            Vertex currentEdgeStartVertex = navigatorListner.CurrentRail.gameObject.GetComponent<Edge>().StartVertex;
            Debug.Log(currentEdgeStartVertex.gameObject.name);
            navigator.getShortestRoute(currentEdgeStartVertex, vertex);
        }

        IEnumerator test()
        {
            while(navigatorListner.CurrentRail == null) yield return null;
            directTo(destination);
        }
    }
}

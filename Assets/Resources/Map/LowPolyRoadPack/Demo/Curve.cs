using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleNavigation
{
    public class Curve :MonoBehaviour
    {
        [SerializeField]
        private Edge _fromEdge;
        [SerializeField]
        private Edge _toEdge;
        private List<Rail> rails;
        private bool isActivate = false;
        private Canvas _routeDisplayer;

        public Edge FromEdge{get{return _fromEdge;}}
        public Edge ToEdge{get{return _toEdge;}}
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            rails = new List<Rail>(this.GetComponentsInChildren<Rail>());
            _routeDisplayer = this.GetComponentInChildren<Canvas>();
        }

        void Update() {
            if(_fromEdge.isActive && _toEdge.isActive && !isActivate)
            {
                if(!isActivate)
                {
                    Activate();
                }
            }
            else if(isActivate)
            {
                if(isActivate)
                {
                    DeActivate();
                }
            }
        }

        private void Activate()
        {
            foreach(Rail rail in rails)
            {
                rail.Activate();
            }
        }

        private void DeActivate()
        {
            foreach(Rail rail in rails)
            {
                rail.DeActivate();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OculusBallGame
{
    public class FailDetector : MonoBehaviour
    {
        public event UnityAction GameOverEvent;

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "Ball")
            {
                if(GameOverEvent != null)
                {
                    GameOverEvent();
                }
            }
        }
    }
}

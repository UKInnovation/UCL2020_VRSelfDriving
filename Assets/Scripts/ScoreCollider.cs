using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OculusBallGame
{
    public class ScoreCollider : MonoBehaviour
    {
        public event UnityAction getOneScore;
        [SerializeField]
        private Text text;
        private int score = 0;

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.name == "Ball")
            {
                // if(getOneScore != null)
                // {
                //     getOneScore();
                // }
                score += 1;
                text.text = score.ToString();
            }
        }
    }
}


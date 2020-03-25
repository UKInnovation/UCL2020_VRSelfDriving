using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OculusBallGame
{
    public class GameBall : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody ball_rb;
        [SerializeField]
        private float magnitude;

        private Vector3 currentDirection = new Vector3(0, 0, 0);


        void FixedUpdate()
        {
            transform.localPosition += currentDirection * magnitude;
        }
        public void OnGameStart()
        {
            Vector3 random_direction = Random.onUnitSphere;
            random_direction = Vector3.ProjectOnPlane(random_direction, transform.forward).normalized;
            random_direction = (random_direction + 1.5f * transform.forward);
            currentDirection = random_direction;
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        private void OnTriggerEnter(Collider other)
        {
            string ObjectName = other.gameObject.name;
            if (ObjectName.Equals("Bottom") || ObjectName.Equals("Upper"))
            {
                currentDirection = new Vector3(currentDirection.x, -currentDirection.y, currentDirection.z);
            }
            else if (ObjectName.Equals("Left") || ObjectName.Equals("Right"))
            {
                currentDirection = new Vector3(-currentDirection.x, currentDirection.y, currentDirection.z);
            }
            else if (ObjectName.Equals("Inner"))
            {
                currentDirection = new Vector3(currentDirection.x, currentDirection.y, -currentDirection.z);
                magnitude += 0.01f;
            }
            else if (!ObjectName.Equals("HandLeft") && !ObjectName.Equals("HandRight") && !ObjectName.Contains("Rail"))
            {
                if (currentDirection.z < 0)
                {
                    currentDirection = new Vector3(currentDirection.x, currentDirection.y, -currentDirection.z);
                }
                else
                {
                    StartCoroutine(Boosting());
                }
            }
        }

        IEnumerator Boosting()
        {
            int counter = 0;
            while (counter < 100)
            {
                counter++;
                transform.localPosition += new Vector3(0, 0, 0.001f);
                yield return null;
            }
        }

        public void OnGameOver()
        {
            Destroy(this.gameObject);
        }
    }
}

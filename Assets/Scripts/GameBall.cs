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
        private int magnitude;

        // Update is called once per frame
        public void OnGameStart()
        {
            Vector3 random_direction = Random.onUnitSphere;
            random_direction = Vector3.ProjectOnPlane(random_direction, transform.forward);
            random_direction = random_direction + transform.forward;
            
            // ball_rb.AddForce(random_direction * magnitude, ForceMode.VelocityChange);
            ball_rb.velocity = random_direction * magnitude;
        }

        public void OnGameOver()
        {
            Destroy(this.gameObject);
        }
    }
}

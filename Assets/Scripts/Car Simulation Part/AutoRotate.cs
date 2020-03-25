using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YoyouOculusFramework
{
    public class AutoRotate : MonoBehaviour
    {
        [SerializeField]
        private float rotatingSpeed = 5f;
        [SerializeField]
        private float x_changeShapeSpeed = 0.1f;
        [SerializeField]
        private float y_changeShapeSpeed = 0.2f;
        [SerializeField]
        private bool changeShape = true;
        private bool x_reach_max_size = false;
        private bool y_reach_max_size = true;

        // Update is called once per frame
        void Update()
        {
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z + rotatingSpeed * Time.deltaTime);

            if (changeShape)
            {
                if (transform.localScale.x < 1.1 && !x_reach_max_size)
                {
                    transform.localScale = transform.localScale + new Vector3(x_changeShapeSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    x_reach_max_size = true;
                }

                if (transform.localScale.x > 0.9 && x_reach_max_size)
                {
                    transform.localScale = transform.localScale - new Vector3(x_changeShapeSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    x_reach_max_size = false;
                }

                if (transform.localScale.y < 1.1 && !y_reach_max_size)
                {
                    transform.localScale = transform.localScale + new Vector3(0, y_changeShapeSpeed * Time.deltaTime, 0);
                }
                else
                {
                    y_reach_max_size = true;
                }

                if (transform.localScale.y > 0.9 && y_reach_max_size)
                {
                    transform.localScale = transform.localScale - new Vector3(0, y_changeShapeSpeed * Time.deltaTime, 0);
                }
                else
                {
                    y_reach_max_size = false;
                }
            }
        }

        public void ChangeRotateSpeed(float speed)
        {
            rotatingSpeed = speed;
        }
    }

}
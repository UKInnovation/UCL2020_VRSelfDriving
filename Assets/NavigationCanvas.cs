using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YoyouOculusFramework
{
    public class NavigationCanvas : MonoBehaviour
    {
        [SerializeField]
        private HandTrackingButton _gridPrefeb;
        [SerializeField]
        private Camera MapCamera;

        public GameObject prefeb;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            initializeGrid();
        }

        private void initializeGrid()
        {
            RectTransform canvsRect = this.GetComponent<RectTransform>();
            float canvas_high = canvsRect.sizeDelta.y;
            float canvas_width = canvsRect.sizeDelta.x;

            RectTransform gridRect = _gridPrefeb.GetComponent<RectTransform>();
            float grid_high = gridRect.sizeDelta.y;
            float grid_width = gridRect.sizeDelta.x;

            Vector2 startPosition = new Vector2(-canvas_width/2 + grid_width/2, canvas_high/2 - grid_high/2);
            Vector2 GoDown = new Vector2(0, -grid_high);
            Vector2 GoRight = new Vector2(grid_width, 0);

            Vector2 currentGridPos = startPosition;

            while(currentGridPos.y > -canvas_high/2)
            {
                while(currentGridPos.x < canvas_width/2)
                {
                    HandTrackingButton grid = Instantiate(_gridPrefeb, transform.localPosition, transform.localRotation);
                    grid.GetComponent<RectTransform>().localScale = canvsRect.localScale;
                    grid.transform.parent = this.transform;
                    grid.GetComponent<RectTransform>().localPosition = currentGridPos;
                    grid.OnEnterActionZone.AddListener(delegate{NavigateTo(currentGridPos);});
                    NavigateTo(currentGridPos);
                    currentGridPos += GoRight;
                }
                currentGridPos.x = startPosition.x;
                currentGridPos += GoDown;
            }
        }

        private void NavigateTo(Vector2 relativePos)
        {
            RectTransform canvsRect = this.GetComponent<RectTransform>();
            float canvas_high = canvsRect.sizeDelta.y;
            float canvas_width = canvsRect.sizeDelta.x;

            float MapWorldScaleRatio = (MapCamera.orthographicSize * 2) / canvas_high;
            Vector2 realWorldrelativePos = relativePos * MapWorldScaleRatio;

            Vector3 CameraHeadingDirection3d = MapCamera.transform.rotation.eulerAngles;
            // Vector2 CameraHeadingDirection2d = new Vector2(CameraHeadingDirection3d.x, CameraHeadingDirection3d.z);

            Debug.Log(relativePos);
            float angle = Mathf.Atan(relativePos.x/relativePos.y);
            if(relativePos.y < 0)
            {
                angle = Mathf.PI + angle;
            }
            angle = angle + (CameraHeadingDirection3d.y * Mathf.PI)/180;
            Vector3 realWorldPos = transform.position + (new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * realWorldrelativePos.magnitude);
            Instantiate(prefeb, realWorldPos, new Quaternion());
        }
    }
}

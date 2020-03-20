using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoyouOculusFramework;
using UnityEngine.UI;
using System;

namespace VehicleNavigation
{
    public class NavigationCanvas : MonoBehaviour
    {
        [SerializeField]
        private HandTrackingButton _gridPrefeb;
        private Camera MapCamera;
        private NavigatorController navigatorController;
        private List<HandTrackingButton> grids = new List<HandTrackingButton>();

        public GameObject prefeb;
        public Text text;

        public Transform carT;
        // [SerializeField]
        // private List<Transform> vertices;

        [SerializeField]
        private HandTrackingButton SizeIncreaseButton;
        [SerializeField]
        private HandTrackingButton SizeDecreaseButton;
        [SerializeField]
        private HandTrackingButton NavigationButton;
        void Awake()
        {
            MapCamera = GameObject.Find("MapCamera").GetComponent<Camera>();
            navigatorController = GameObject.Find("NavigationController").GetComponent<NavigatorController>();
            NavigationButton.OnExitActionZone.AddListener(OnNavigationButtonPressed);
            SizeIncreaseButton.OnStayInActionZone.AddListener(OnIncreseButtonHold);
            SizeDecreaseButton.OnStayInActionZone.AddListener(OnDecreaseButtonHold);
            text = GameObject.Find("Text (3)").GetComponent<Text>();
            carT = GameObject.FindGameObjectWithTag("car").transform;
            initializeGrid();
            SetActivateGrids(false);
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            StartCoroutine(test());
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
                    grids.Add(grid);
                    grid.GetComponent<RectTransform>().localScale = canvsRect.localScale;
                    grid.transform.SetParent(this.transform);
                    grid.GetComponent<RectTransform>().localPosition = currentGridPos;
                    grid.OnEnterActionZone.AddListener(delegate{NavigateTo(currentGridPos);});
                    // NavigateTo(currentGridPos);
                    currentGridPos += GoRight;
                }
                currentGridPos.x = startPosition.x;
                currentGridPos += GoDown;
            }
        }

        private void NavigateTo(Vector2 relativePos)
        {
            SetActivateGrids(false);
            RectTransform canvsRect = this.GetComponent<RectTransform>();
            float canvas_high = canvsRect.sizeDelta.y;
            float canvas_width = canvsRect.sizeDelta.x;

            float MapWorldScaleRatio = (MapCamera.orthographicSize * 2) / canvas_high;
            Vector2 realWorldrelativePos = relativePos * MapWorldScaleRatio;

            Vector3 CameraHeadingDirection3d = MapCamera.transform.rotation.eulerAngles;
            // Vector2 CameraHeadingDirection2d = new Vector2(CameraHeadingDirection3d.x, CameraHeadingDirection3d.z);

            // Debug.Log(relativePos);
            float angle = Mathf.Atan(relativePos.x/relativePos.y);
            if(relativePos.y < 0)
            {
                angle = Mathf.PI + angle;
            }

            angle = angle + (CameraHeadingDirection3d.y * Mathf.PI)/180;
            Debug.Log(carT.position);
            Vector3 realWorldPos = carT.position + (new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * realWorldrelativePos.magnitude);
            navigatorController.NavigateTo(realWorldPos);
            // GameObject cude = Instantiate(prefeb, realWorldPos, new Quaternion());
            // cude.transform.localScale = new Vector3(2,2,2);
        }

        private void OnNavigationButtonPressed()
        {
            SetActivateGrids(true);
        }

        private void SetActivateGrids(bool activate)
        {
            if(activate)
            {
                foreach(HandTrackingButton grid in grids)
                {
                    grid.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach(HandTrackingButton grid in grids)
                {
                    grid.gameObject.SetActive(false);
                }
            }
        }

        private void ChangeMapSize(float amount)
        {
            MapCamera.orthographicSize += amount;
            if(MapCamera.orthographicSize < 5){
                MapCamera.orthographicSize = 5;
            }
            else if(MapCamera.orthographicSize > 100){
                MapCamera.orthographicSize = 100;
            }
        }

        private void OnIncreseButtonHold()
        {
            ChangeMapSize(-0.3f);
        }

        private void OnDecreaseButtonHold()
        {
            ChangeMapSize(0.3f);
        }

        IEnumerator test(){
            yield return new WaitForSeconds(2);
            navigatorController.NavigateTo(new Vector3(-240.6f, 0, 57.5f));
                        // Instantiate(prefeb, new Vector3(-98f, 0, -271f), new Quaternion());
            
        }
    }
}

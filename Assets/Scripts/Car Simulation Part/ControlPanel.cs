using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace YoyouOculusFramework
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField]
        private Canvas startPage;
        [SerializeField]
        private Canvas navigationPage;
        [SerializeField]
        private Canvas musicPage;
        [SerializeField]
        private Canvas gamePage;
        [SerializeField]
        private HandTrackingButton startPageButton;
        [SerializeField]
        private HandTrackingButton NavigationPageButton;
        [SerializeField]
        private HandTrackingButton musicPageButton;
        [SerializeField]
        private HandTrackingButton gamePageButton;
        private RectTransform PageAnchor;
        private RectTransform MainPageButtonAnchor;
        private RectTransform NavigationPageButtonAnchor;
        private RectTransform MusicPageButtonAnchor;
        private RectTransform GamePageButtonAnchor;
        private Canvas currentPage;
        // public UnityEvent[] turnPageEvents;\

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            PageAnchor = transform.Find("Page Anchor").GetComponent<RectTransform>();
            MainPageButtonAnchor = transform.Find("Main Page Button Anchor").GetComponent<RectTransform>();
            NavigationPageButtonAnchor = transform.Find("Camera Page Button Anchor").GetComponent<RectTransform>();
            MusicPageButtonAnchor = transform.Find("Music Page Button Anchor").GetComponent<RectTransform>();
            GamePageButtonAnchor = transform.Find("Game Page Button Anchor").GetComponent<RectTransform>();
        }
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            initialize_Canvas(ref startPage, PageAnchor);
            initialize_Canvas(ref navigationPage, PageAnchor);
            initialize_Canvas(ref musicPage, PageAnchor);
            initialize_Canvas(ref gamePage, PageAnchor);
            gamePage.gameObject.SetActive(false);
            musicPage.gameObject.SetActive(false);
            navigationPage.gameObject.SetActive(false);

            initialize_Button(ref startPageButton, MainPageButtonAnchor, delegate { toStartPage(); });
            initialize_Button(ref NavigationPageButton, NavigationPageButtonAnchor, delegate { toNavigationPage(); });
            initialize_Button(ref musicPageButton, MusicPageButtonAnchor, delegate { toMusicPage(); });
            initialize_Button(ref gamePageButton, GamePageButtonAnchor, delegate { toGamePage(); });
            currentPage = startPage;
        }

        private void initialize_Canvas(ref Canvas element, RectTransform Anchor)
        {
            element = Instantiate(element);
            element.transform.SetParent(transform);
            element.GetComponent<RectTransform>().localPosition = Anchor.GetComponent<RectTransform>().localPosition;
            element.GetComponent<RectTransform>().localRotation = Anchor.GetComponent<RectTransform>().localRotation;
            element.GetComponent<RectTransform>().localScale = Anchor.GetComponent<RectTransform>().localScale;
        }

        private void initialize_Button(ref HandTrackingButton button, RectTransform Anchor, UnityAction call)
        {
            button = Instantiate(button);
            button.transform.SetParent(transform);
            button.GetComponent<RectTransform>().localPosition = Anchor.GetComponent<RectTransform>().localPosition;
            button.GetComponent<RectTransform>().localRotation = Anchor.GetComponent<RectTransform>().localRotation;
            button.GetComponent<RectTransform>().localScale = Anchor.GetComponent<RectTransform>().localScale;
            button.OnEnterActionZone.AddListener(call);
        }

        private void closePage(Canvas page)
        {
            if (page.GetComponent<MusicPlayer>() == null)
            {
                page.gameObject.SetActive(false);
            }
            else
            {
                page.GetComponent<MusicPlayer>().DisableMusicCanvas();
            }
        }

        private void openPage(Canvas page)
        {
            page.gameObject.SetActive(true);
            if (page.GetComponent<MusicPlayer>() != null)
            {
                page.GetComponent<MusicPlayer>().EnableMusicCanvas();
            }
        }

        public void toStartPage()
        {
            closePage(currentPage);
            openPage(startPage);
            currentPage = startPage;
        }
        public void toNavigationPage()
        {
            closePage(currentPage);
            openPage(navigationPage);
            currentPage = navigationPage;
        }
        public void toMusicPage()
        {
            closePage(currentPage);
            openPage(musicPage);
            currentPage = musicPage;
        }
        public void toGamePage()
        {

            closePage(currentPage);
            openPage(gamePage);
            currentPage = gamePage;

        }
        public void DisableButtons()
        {
            startPageButton.gameObject.SetActive(false);
            NavigationPageButton.gameObject.SetActive(false);
            musicPageButton.gameObject.SetActive(false);
            gamePageButton.gameObject.SetActive(false);
        }

        public void EnableButtons()
        {
            startPageButton.gameObject.SetActive(true);
            NavigationPageButton.gameObject.SetActive(true);
            musicPageButton.gameObject.SetActive(true);
            gamePageButton.gameObject.SetActive(true);
        }
    }

}

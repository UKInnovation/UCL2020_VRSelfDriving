using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework;

namespace YoyouOculusFramework
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField]
        private Song[] songs;
        private AudioSource player;
        [SerializeField]
        private PlayButton playButton;
        [SerializeField]
        private RawImage songCoverImage;
        [SerializeField]
        private Image progressionBar;
        private int songIndex = 0;
        [SerializeField]
        private Transform progressionController;
        public Text text; 
        private bool isControllerGrabbed = false;
        void Awake() 
        {
            player = transform.GetComponent<AudioSource>();   
        }
        void Update() {
            if(player.time >= songs[songIndex].clip.length - 0.1)
            {
                nextSong();
            }
            if(!isControllerGrabbed){
                progressionBar.fillAmount = player.time / songs[songIndex].clip.length;
                progressionController.localPosition = new Vector3(-300 + progressionBar.fillAmount * 600, -130, 0);   
            }else{
                progressionBar.fillAmount = (progressionController.localPosition.x + 300) / 600;
            }
        }
        void Start()
        {
            player.clip = songs[0].clip;
            songCoverImage.texture = songs[0].coverImage;
            pause();
        }
        public void nextSong()
        {
            songIndex++;
            songIndex = songIndex % songs.Length;
            player.clip = songs[songIndex].clip;
            player.time = 0;
            songCoverImage.texture = songs[songIndex].coverImage;
            play();
        }
        public void prevSong()
        {
            songIndex--;
            if(songIndex < 0) songIndex += songs.Length;
            player.clip = songs[songIndex].clip;
            player.time = 0;
            songCoverImage.texture = songs[songIndex].coverImage;
            play();
        }
        public void  pause()
        {
            player.Pause();
            playButton.toPausecIcon();
        }
        public void play()
        {
            player.Play();
            playButton.toPlayIcon();
        }

        public void OnControllerGrabbed(){
            isControllerGrabbed = true;
            pause();
        }

        public void OnControllerReleased()
        {
            player.time = songs[songIndex].clip.length * ((progressionController.localPosition.x + 300) / 600);
            isControllerGrabbed = false;
            play();
        }

        public void OnPlayButtonClicked()
        {
            if(player.isPlaying)
            {
                pause();
            }
            else
            {
                play();
            }
        }

        public void DisableMusicCanvas()
        {
            this.GetComponent<Canvas>().enabled = false;
            playButton.gameObject.SetActive(false);
            transform.Find("Prev Song Button").gameObject.SetActive(false);
            transform.Find("Next Song Button").gameObject.SetActive(false);
            transform.Find("Progress Controller").gameObject.SetActive(false);
        }

        public void EnableMusicCanvas()
        {
            this.GetComponent<Canvas>().enabled = true;
            playButton.gameObject.SetActive(true);
            transform.Find("Prev Song Button").gameObject.SetActive(true);
            transform.Find("Next Song Button").gameObject.SetActive(true);
            transform.Find("Progress Controller").gameObject.SetActive(true);
        }
    }
}

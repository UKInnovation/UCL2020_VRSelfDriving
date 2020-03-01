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
            text.text = progressionController.localPosition.x.ToString() + " " + progressionController.localPosition.y.ToString() + " " + progressionController.localPosition.z.ToString();
            if(player.time >= songs[songIndex].clip.length - 0.1)
            {
                nextSong();
            }
            if(!isControllerGrabbed){
                progressionBar.fillAmount = player.time / songs[songIndex].clip.length;
                progressionController.localPosition = new Vector3(-200 + progressionBar.fillAmount * 400, -66, 0);   
            }else{
                progressionBar.fillAmount = (progressionController.localPosition.x + 200) / 400;
            }
        }
        void Start()
        {
            player.clip = songs[0].clip;
            songCoverImage.texture = songs[0].coverImage;
            play();
        }
        public void nextSong()
        {
            songIndex++;
            songIndex = songIndex % songs.Length;
            player.clip = songs[songIndex].clip;
            songCoverImage.texture = songs[songIndex].coverImage;
            play();
        }
        public void prevSong()
        {
            songIndex--;
            if(songIndex < 0) songIndex += songs.Length;
            player.clip = songs[songIndex].clip;
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
            player.time = songs[songIndex].clip.length * ((progressionController.localPosition.x + 200) / 400);
            isControllerGrabbed = false;
            play();
        }
    }
}

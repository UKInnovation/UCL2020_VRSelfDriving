using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource[] songList;

    public Sprite[] buttons;
    private int SongIndex = 0;

    private AudioSource player;

    void Start()
    {
         player = GameObject.Instantiate(songList[0]);
         GameObject playButton = transform.Find("PlayButton").gameObject;
         playButton.GetComponent<Image>().sprite = buttons[0];
    }

    public void nextSong(){
        GameObject.Destroy(player.gameObject);
        SongIndex += 1;
        SongIndex = SongIndex % songList.Length;
        player = GameObject.Instantiate(songList[SongIndex]);
    }

        public void prevSong(){
        GameObject.Destroy(player.gameObject);
        SongIndex--;
        if(SongIndex < 0){
            SongIndex += songList.Length;
        }
        player = GameObject.Instantiate(songList[SongIndex]);
    }

        public void plarOrStop(){
            GameObject playButton = transform.Find("PlayButton").gameObject;

            if(player.isPlaying){
                playButton.GetComponent<Image>().sprite = buttons[1];
                player.Pause();
            }else{
                playButton.GetComponent<Image>().sprite = buttons[0];
                player.Play();
            }
        } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGM_demo : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        slider.value = 1;
        audioSource.loop = true;
        audioSource.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //change volume of background music
        audioSource.volume = slider.value;
    }

    //start or stop the music
    public void play_stop_music()
    {
        Debug.Log("s");
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    //pause
    public void pause_music()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}

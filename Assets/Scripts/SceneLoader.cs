using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SceneLoader : MonoBehaviour
{
    public Image LoadingBar;
    public int SceneIndex;
    public Text percentage;

    void Start()
    {
        StartCoroutine(LoadSceneAsyn(SceneIndex));
    }


    IEnumerator LoadSceneAsyn(int SceneIndex)
    {
        float counter = 0;
        while(counter <= 1000)
        {
            float progress = counter / 1000;
            LoadingBar.fillAmount = progress;
            percentage.text = (Convert.ToInt32(counter/10)).ToString() + "%";
            counter++;
            yield return null;
        } 
        SceneManager.LoadScene("Car Simulation Scene");
    }
}

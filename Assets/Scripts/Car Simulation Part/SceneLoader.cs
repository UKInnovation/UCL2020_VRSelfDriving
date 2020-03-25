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
    public string SceneName;

    void Start()
    {
        StartCoroutine(LoadScene(SceneIndex));
    }


    IEnumerator LoadScene(int SceneIndex)
    {
        float counter = 0;
        while (counter <= 1000)
        {
            float progress = counter / 1000;
            LoadingBar.fillAmount = progress;
            percentage.text = (Convert.ToInt32(counter / 10)).ToString() + "%";
            counter += 1;
            yield return null;
        }
        SceneManager.LoadScene(SceneName);
    }
}

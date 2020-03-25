using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSelectionSceneLoader : MonoBehaviour
{
    public void BackToSelectionScene()
    {
        SceneManager.LoadScene("Car Selection Scene");
    }
}

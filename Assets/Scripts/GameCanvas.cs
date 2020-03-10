using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusBallGame;
public class GameCanvas : MonoBehaviour
{
    public BallGame ballGamePrefab;
    public float gamePosOffset;
    public Text message;
    private BallGame ballGame;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    // void Start()
    // {
    //     LoadGame();
    // }

    public void LoadGame()
    {
        this.gameObject.SetActive(false);
        ballGame = Instantiate(ballGamePrefab, transform.position + transform.forward * gamePosOffset, transform.rotation);
        ballGame.transform.SetParent(transform.parent);
        ballGame.GameOverEvent += OnGameOver;
    }

    void OnGameOver()
    {
        message.text = "Your Score: " + ballGame.score.ToString();
        Destroy(ballGame.gameObject);
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    // void OnDisable()
    // {
    //     if(ballGame != null)
    //     {
    //         ballGame.gameObject.SetActive(false);
    //     }
    // }

    // private void OnEnable() 
    // {
    //     if(ballGame != null)
    //     {
    //         ballGame.gameObject.SetActive(true);
    //     }    
    // }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>/

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
}

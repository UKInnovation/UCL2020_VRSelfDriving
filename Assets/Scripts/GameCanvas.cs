using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusBallGame;
public class GameCanvas : MonoBehaviour
{
    public BallGame ballGamePrefab;
    public Vector3 gamePosOffset;
    private BallGame ballGame;

    public void LoadGame()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        ballGame = Instantiate(ballGamePrefab, transform.position + gamePosOffset, transform.rotation);
        // ballGame.transform.SetParent(this.transform);
        ballGame.GameOverEvent += OnGameOver;
    }

    void OnGameOver()
    {
        Destroy(ballGame.gameObject);
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>/

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
}

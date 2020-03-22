using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusBallGame;
using YoyouOculusFramework;
public class GameCanvas : MonoBehaviour
{
    public BallGame ballGamePrefab;
    public float gamePosOffset;
    public Text message;
    private BallGame ballGame;


    public void LoadGame()
    {
        this.gameObject.SetActive(false);
        transform.parent.GetComponent<ControlPanel>().DisableButtons();
        ballGame = Instantiate(ballGamePrefab, transform.position + transform.forward * gamePosOffset + new Vector3(0, 0.1f, 0), transform.rotation * Quaternion.Euler(-15, 0, 0));
        ballGame.transform.SetParent(transform.parent);
        ballGame.GameOverEvent += OnGameOver;
    }

    void OnGameOver()
    {
        message.text = "Your Score: " + ballGame.score.ToString();
        Destroy(ballGame.gameObject);
        this.gameObject.SetActive(true);
        transform.parent.GetComponent<ControlPanel>().EnableButtons();
    }

}
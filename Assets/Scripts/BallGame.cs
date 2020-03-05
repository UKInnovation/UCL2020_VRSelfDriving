using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OculusBallGame
{
    public class BallGame : MonoBehaviour
    {
        private GameBall gameBall;
        private FailDetector failDetector;

        public event UnityAction GameOverEvent;

        private int score;

        void Awake() 
        {
            gameBall = transform.GetComponentInChildren<GameBall>();
            failDetector = transform.GetComponentInChildren<FailDetector>();

            failDetector.GameOverEvent += this.OnGameOver;
            failDetector.GameOverEvent += gameBall.OnGameOver;
        }

        void Start()
        {
            gameBall.OnGameStart();
        }

        private void OnGameOver()
        {
            Destroy(gameBall);
            if(GameOverEvent != null)
            {
                GameOverEvent.Invoke();
            }
        }
    }
}

using System;
using _Minigame;
using _MiniGame.Scripts.EndGameConditions;
using UnityEngine;

namespace _MiniGame
{
    public class GameMode
    {
        public event Action GameWon;
        public event Action GameLost;

        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly EndGameChecker _endGameChecker;

        private bool _isRunning;

        public GameMode
        (
            EnemiesSpawner enemiesSpawner,
            EndGameChecker endGameChecker
        )
        {
            _enemiesSpawner = enemiesSpawner;
            _endGameChecker = endGameChecker;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false) return;

            _enemiesSpawner?.Update(deltaTime);

            EndGameStatus result = _endGameChecker.GetStatus(deltaTime);

            if (result == EndGameStatus.GameLost)
            {
                ProcessDefeat();
            }

            if (result == EndGameStatus.GameWon)
            {
                ProcessVictory();
            }
        }

        public void Start()
        {
            _isRunning = true;
        }

        private void ProcessVictory()
        {
            ProcessEndGame();

            GameWon?.Invoke();

            Debug.Log("Game won");
        }

        private void ProcessDefeat()
        {
            ProcessEndGame();

            GameLost?.Invoke();

            Debug.Log("Game lost");
        }

        private void ProcessEndGame()
        {
            _isRunning = false;
        }
    }
}
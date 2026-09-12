using System;
using System.Collections.Generic;
using _Minigame;
using UnityEngine;

namespace _MiniGame
{
    public class GameMode
    {
        public event Action GameWon;
        public event Action GameLost;

        private readonly Character _hero;
        private EnemiesSpawner _enemiesSpawner;
        private LevelConfig _levelConfig;

        private bool _isRunning;
        private float _timePassed;

        private List<Func<bool>> _winConditions = new();
        private List<Func<bool>> _loseConditions = new();

        public GameMode
        (
            Character hero,
            EnemiesSpawner enemiesSpawner,
            LevelConfig levelConfig,
            WinConditionType winConditionType,
            LoseConditionType loseConditionType
        )
        {
            _hero = hero;

            _levelConfig = levelConfig;
            _enemiesSpawner = enemiesSpawner;

            SetWinConditions(winConditionType);
            SetLoseConditions(loseConditionType);
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false) return;

            _enemiesSpawner?.Update(deltaTime);

            _timePassed += deltaTime;

            if (IsGameLost(_loseConditions))
            {
                ProcessDefeat();
            }

            if (IsGameWon(_winConditions))
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


        #region SetConditions

        private void SetWinConditions(WinConditionType winConditionType)
        {
            Func<bool> secondsPassedCondition = () =>
                _timePassed >= _levelConfig.SecondsToSurvive;

            Func<bool> enemiesDefeatedCondition = () =>
                _enemiesSpawner.DeadEnemyCount >= _levelConfig.DefeatEnemiesToWin;

            switch (winConditionType)
            {
                case WinConditionType.DefeatNEnemies:
                    _winConditions.Add(enemiesDefeatedCondition);
                    break;

                case WinConditionType.SurviveNSeconds:
                    _winConditions.Add(secondsPassedCondition);
                    break;
            }
        }

        private void SetLoseConditions(LoseConditionType loseConditionType)
        {
            Func<bool> heroDeadCondition = () => _hero.IsDead;
            Func<bool> nEnemiesSpawnedCondition = () =>
                _enemiesSpawner.EnemyCount >= _levelConfig.EnemiesSpawnedToLose;

            switch (loseConditionType)
            {
                case LoseConditionType.HeroIsDead:
                    _loseConditions.Add(heroDeadCondition);
                    break;

                case LoseConditionType.NEnemiesSpawned:
                    _loseConditions.Add(nEnemiesSpawnedCondition);
                    break;
            }
        }

        #endregion

        private bool IsGameLost(List<Func<bool>> conditions)
        {
            foreach (Func<bool> condition in conditions)
            {
                if (condition()) return true;
            }

            return false;
        }

        private bool IsGameWon(List<Func<bool>> conditions)
        {
            foreach (Func<bool> condition in conditions)
            {
                if (condition() == false) return false;
            }

            return true;
        }
    }
}
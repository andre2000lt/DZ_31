using System.Collections;
using _Minigame;
using _MiniGame.Scripts.EndGameConditions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _MiniGame
{
    public class GamePlayCycle
    {
        private readonly ConfirmWindow _confirmWindow;
        private readonly EndGameWindow _lostGameWindow;
        private readonly EndGameWindow _wonGameWindow;

        private readonly LevelConfig _levelConfig;
        private GameMode _gameMode;

        private readonly HeroSpawner _heroSpawner;
        private readonly HeroConfig _heroConfig;

        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly EndGameChecker _endGameChecker;


        private WinConditionType _winConditionType;
        private LoseConditionType _loseConditionType;

        private readonly ControllersUpdateService _controllersUpdateService;
        private readonly MonoBehaviour _coroutineRunner;

        public GamePlayCycle(
            ConfirmWindow confirmWindow,
            LevelConfig levelConfig,
            HeroSpawner heroSpawner,
            HeroConfig heroConfig,
            EnemiesSpawner enemiesSpawner,
            ControllersUpdateService controllersUpdateService,
            EndGameChecker endGameChecker,
            MonoBehaviour coroutineRunner)
        {
            _confirmWindow = confirmWindow;
            _levelConfig = levelConfig;
            _heroSpawner = heroSpawner;
            _heroConfig = heroConfig;
            _enemiesSpawner = enemiesSpawner;
            _endGameChecker = endGameChecker;
            _controllersUpdateService = controllersUpdateService;

            _coroutineRunner = coroutineRunner;

            Canvas canvasPrefab = Resources.Load<Canvas>("Prefabs/Canvas");
            Canvas canvas = Object.Instantiate(canvasPrefab);

            EndGameWindow lostGameWindowPrefab = Resources.Load<EndGameWindow>("Prefabs/LostGameWindow");
            _lostGameWindow = Object.Instantiate(lostGameWindowPrefab, canvas.transform);

            EndGameWindow wonGameWindowPrefab = Resources.Load<EndGameWindow>("Prefabs/WonGameWindow");
            _wonGameWindow = Object.Instantiate(wonGameWindowPrefab, canvas.transform);
        }

        public IEnumerator Launch()
        {
            _controllersUpdateService.Reset();
            _enemiesSpawner.Reset();
            _heroSpawner.Reset();

            Character hero = _heroSpawner.Spawn(_heroConfig);

            _gameMode = new GameMode(_enemiesSpawner, _endGameChecker);
            _gameMode.GameLost += OnGameLost;
            _gameMode.GameWon += OnGameWon;

            _confirmWindow.Show();
            yield return _confirmWindow.ConfirmProcess(KeyCode.F);

            _gameMode.Start();
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        private void OnGameLost()
        {
            _coroutineRunner.StartCoroutine(GameLostProcess());
        }

        private IEnumerator GameLostProcess()
        {
            _gameMode.GameLost -= OnGameLost;

            yield return _lostGameWindow.Show();

            yield return Launch();
        }

        private void OnGameWon()
        {
            _coroutineRunner.StartCoroutine(GameWonProcess());
        }

        private IEnumerator GameWonProcess()
        {
            _gameMode.GameWon -= OnGameWon;

            yield return _wonGameWindow.Show();

            SceneManager.LoadScene((int)SceneType.Menu);
        }
    }
}
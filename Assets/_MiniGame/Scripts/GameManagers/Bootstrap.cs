using System.Collections;
using _Minigame;
using _MiniGame.Scripts.EndGameConditions;
using UnityEngine;

namespace _MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingWindow _loadingWindow;
        [SerializeField] private ConfirmWindow _confirmWindow;

        private ControllersFactory _controllersFactory;
        private ControllersUpdateService _controllersUpdateService;
        private CharacterFactory _characterFactory;
        private EndGameConditionsFactory _endGameConditionsFactory;
        private EndGameChecker _endGameChecker;

        private HeroSpawner _heroSpawner;
        private EnemiesSpawner _enemiesSpawner;

        private GamePlayCycle _gamePlayCycle;


        private void Awake()
        {
            StartCoroutine(StartProcess());
        }

        private void Update()
        {
            _controllersUpdateService?.Update(Time.deltaTime);
            _gamePlayCycle?.Update(Time.deltaTime);
        }

        private IEnumerator StartProcess()
        {
            _loadingWindow.Show();

            LevelConfigCollection levelConfigs = Resources.Load<LevelConfigCollection>("Configs/LevelConfigCollection");
            LevelConfig levelConfig = levelConfigs.GetRandom();

            _controllersFactory = new ControllersFactory();
            _controllersUpdateService = new ControllersUpdateService();
            _characterFactory = new CharacterFactory(_controllersUpdateService, _controllersFactory);

            _heroSpawner = new HeroSpawner(levelConfig, _characterFactory, _controllersUpdateService);
            HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");

            _enemiesSpawner = new EnemiesSpawner(levelConfig, _characterFactory, _controllersUpdateService, this);

            _endGameConditionsFactory = new EndGameConditionsFactory(_heroSpawner, _enemiesSpawner, levelConfig);
            _endGameChecker = new EndGameChecker(levelConfig, _endGameConditionsFactory);


            _gamePlayCycle = new GamePlayCycle
            (
                _confirmWindow,
                levelConfig,
                _heroSpawner,
                heroConfig,
                _enemiesSpawner,
                _controllersUpdateService,
                _endGameChecker,
                this
            );

            yield return new WaitForSeconds(1.5f);

            _loadingWindow.Hide();

            yield return _gamePlayCycle.Launch();
        }
    }
}
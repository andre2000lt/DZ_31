using System.Collections;
using _Minigame;
using UnityEngine;

namespace _MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingWindow _loadingWindow;
        [SerializeField] private ConfirmWindow _confirmWindow;
        [SerializeField] private WinConditionType _winConditionType;
        [SerializeField] private LoseConditionType _loseConditionType;

        private ControllersFactory _controllersFactory;
        private ControllersUpdateService _controllersUpdateService;
        private CharacterFactory _characterFactory;

        private HeroSpawner _heroSpawner;
        private EnemiesSpawner _enemiesSpawner;

        private GamePlayCircle _gamePlayCircle;


        private void Awake()
        {
            StartCoroutine(StartProcess());
        }

        private void Update()
        {
            _controllersUpdateService?.Update(Time.deltaTime);
            _gamePlayCircle?.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
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

            _gamePlayCircle = new GamePlayCircle
            (
                _confirmWindow,
                levelConfig,
                _heroSpawner,
                heroConfig,
                _enemiesSpawner,
                _winConditionType,
                _loseConditionType,
                _controllersUpdateService,
                this
            );

            yield return new WaitForSeconds(1.5f);

            _loadingWindow.Hide();

            yield return _gamePlayCircle.Launch();
        }
    }
}
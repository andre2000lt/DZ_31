using System.Collections.Generic;
using System.Linq;
using _MiniGame;
using UnityEngine;

namespace _Minigame
{
    public class EnemiesSpawner
    {
        private float _timer;
        private readonly EnemyConfig _enemyConfig;

        private readonly CharacterFactory _characterFactory;
        private readonly ControllersUpdateService _controllersUpdateService;

        private readonly IReadOnlyList<Vector3> _spawnPoints;
        private readonly Character _prefab;
        private readonly LevelConfig _levelConfig;

        private readonly List<Character> _enemies = new();

        public EnemiesSpawner
        (
            LevelConfig levelConfig,
            CharacterFactory characterFactory,
            ControllersUpdateService controllersUpdateService,
            MonoBehaviour coroutineRunner)
        {
            _characterFactory = characterFactory;
            _controllersUpdateService = controllersUpdateService;

            _levelConfig = levelConfig;
            _spawnPoints = _levelConfig.EnemiesSpawnPoints;

            _prefab = Resources.Load<Character>("Prefabs/Enemy");

            _enemyConfig = _levelConfig.EnemyConfig;
        }

        public int EnemyCount => _enemies.Count(enemy => enemy.IsDead == false);
        public int DeadEnemyCount => _enemies.Count(enemy => enemy.IsDead);

        public void Update(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0f)
            {
                Spawn();

                _timer = _levelConfig.EnemySpawnInterval;
            }
        }

        public void Reset()
        {
            foreach (Character enemy in _enemies)
            {
                enemy.Dead -= OnEnemyDead;
                Object.Destroy(enemy.gameObject);
            }

            _enemies.Clear();

            _timer = _levelConfig.EnemySpawnInterval;
        }

        private void Spawn()
        {
            Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

            Character enemy = _characterFactory.CreateEnemy
            (
                _prefab,
                _enemyConfig.Health,
                _enemyConfig.RotationSpeed,
                _enemyConfig.Speed,
                spawnPoint,
                _enemyConfig.ChangeDirectionInterval
            );

            _enemies.Add(enemy);
            enemy.Dead += OnEnemyDead;
        }

        private void OnEnemyDead(Character enemy)
        {
            _controllersUpdateService.Disable(enemy);
        }
    }
}
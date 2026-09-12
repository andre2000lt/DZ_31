using UnityEngine;
using Object = UnityEngine.Object;

namespace _MiniGame
{
    public class HeroSpawner
    {
        private readonly Character _prefab;
        private readonly Vector3 _spawnPoint;

        private readonly CharacterFactory _characterFactory;
        private ControllersUpdateService _controllersUpdateService;

        private Character _hero;

        public HeroSpawner
        (
            LevelConfig levelConfig,
            CharacterFactory characterFactory,
            ControllersUpdateService controllersUpdateService
        )
        {
            _prefab = Resources.Load<Character>("Prefabs/Hero");
            _spawnPoint = levelConfig.HeroSpawnPoint;
            _characterFactory = characterFactory;
            _controllersUpdateService = controllersUpdateService;
        }

        public Character Spawn(HeroConfig config)
        {
            _hero = _characterFactory.CreateHero
            (
                _prefab,
                config.HealthAmount,
                config.RotationSpeed,
                config.Speed,
                _spawnPoint
            );

            _hero.Dead += OnHeroDead;

            return _hero;
        }

        public void Reset()
        {
            if (_hero == null) return;

            _hero.Dead -= OnHeroDead;
            Object.Destroy(_hero.gameObject);
        }

        private void OnHeroDead(Character hero)
        {
            _controllersUpdateService.Disable(hero);
        }
    }
}
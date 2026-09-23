using UnityEngine;
using Object = UnityEngine.Object;

namespace _MiniGame
{
    public class HeroSpawner
    {
        private readonly Hero _prefab;
        private readonly Vector3 _spawnPoint;

        private readonly CharacterFactory _characterFactory;
        private ControllersUpdateService _controllersUpdateService;

        public HeroSpawner
        (
            LevelConfig levelConfig,
            CharacterFactory characterFactory,
            ControllersUpdateService controllersUpdateService
        )
        {
            _prefab = Resources.Load<Hero>("Prefabs/Hero");
            _spawnPoint = levelConfig.HeroSpawnPoint;
            _characterFactory = characterFactory;
            _controllersUpdateService = controllersUpdateService;
        }

        public Hero Hero { get; private set; }

        public Hero Spawn(HeroConfig config)
        {
            Hero = _characterFactory.CreateHero
            (
                _prefab,
                config.HealthAmount,
                config.RotationSpeed,
                config.Speed,
                _spawnPoint
            );

            Hero.Dead += OnHeroDead;

            return Hero;
        }

        public void Reset()
        {
            if (Hero == null) return;

            Hero.Dead -= OnHeroDead;
            Object.Destroy(Hero.gameObject);
        }

        private void OnHeroDead(Character hero)
        {
            _controllersUpdateService.Disable(hero);
        }
    }
}
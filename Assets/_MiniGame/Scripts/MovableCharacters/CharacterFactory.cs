using Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _MiniGame
{
    public class CharacterFactory
    {
        private ControllersUpdateService _controllersUpdateService;
        private ControllersFactory _controllersFactory;

        public CharacterFactory(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory)
        {
            _controllersUpdateService = controllersUpdateService;
            _controllersFactory = controllersFactory;
        }

        public Hero CreateHero
        (
            Hero prefab,
            int healthAmount,
            float rotationSpeed,
            float speed,
            Vector3 spawnPosition
        )
        {
            Hero hero = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);
            Rotator rotator = new(hero.transform, rotationSpeed);
            Bullet bulletPrefab = Resources.Load<Bullet>("Prefabs/Bullet");

            hero.Initialize(healthAmount, rotator, speed, bulletPrefab);

            CharacterView characterView = hero.GetComponent<CharacterView>();
            characterView.Initialize(hero);

            CinemachineVirtualCamera virtualCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/Virtual Camera");
            CinemachineVirtualCamera cinemachine = Object.Instantiate(virtualCameraPrefab);
            cinemachine.Follow = hero.transform;

            UserMoveController moveController = _controllersFactory.CreateUserMoveController(hero);
            UserShootController shootController = _controllersFactory.CreateShootController(hero);
            ComposireController userCompositeController =
                _controllersFactory.CreateShootAndMoveController(shootController, moveController);

            userCompositeController.Enable();
            _controllersUpdateService.Add(hero, userCompositeController);

            return hero;
        }


        public Enemy CreateEnemy
        (
            Enemy prefab,
            int healthAmount,
            float rotationSpeed,
            float speed,
            Vector3 spawnPosition,
            float changeDirectionInterval
        )
        {
            Enemy enemy = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);
            Rotator rotator = new(enemy.transform, rotationSpeed);
            enemy.Initialize(healthAmount, rotator, speed);

            CharacterView characterView = enemy.GetComponent<CharacterView>();
            characterView.Initialize(enemy);

            RandomeMovementController controller = _controllersFactory.CreateEnemyController(enemy, changeDirectionInterval);
            controller.Enable();
            _controllersUpdateService.Add(enemy, controller);

            return enemy;
        }
    }
}
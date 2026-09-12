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

        public Character CreateHero
        (
            Character prefab,
            int healthAmount,
            float rotationSpeed,
            float speed,
            Vector3 spawnPosition
        )
        {
            Character hero =
                CreateCharacter(prefab, healthAmount, rotationSpeed, speed, spawnPosition);

            CinemachineVirtualCamera virtualCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/Virtual Camera");
            CinemachineVirtualCamera cinemachine = Object.Instantiate(virtualCameraPrefab);
            cinemachine.Follow = hero.transform;

            UserMoveController controller = _controllersFactory.CreateUserMoveController(hero);
            controller.Enable();
            _controllersUpdateService.Add(hero, controller);

            return hero;
        }


        public Character CreateEnemy
        (
            Character prefab,
            int healthAmount,
            float rotationSpeed,
            float speed,
            Vector3 spawnPosition,
            float changeDirectionInterval
        )
        {
            Character enemy =
                CreateCharacter(prefab, healthAmount, rotationSpeed, speed, spawnPosition);

            EnemyController controller = _controllersFactory.CreateEnemyController(enemy, changeDirectionInterval);
            controller.Enable();
            _controllersUpdateService.Add(enemy, controller);

            return enemy;
        }

        private Character CreateCharacter
        (
            Character prefab,
            int healthAmount,
            float rotationSpeed,
            float speed,
            Vector3 spawnPosition
        )
        {
            Character character = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);
            Rotator rotator = new(character.transform, rotationSpeed);
            character.Initialize(healthAmount, rotator, speed);

            CharacterView characterView = character.GetComponent<CharacterView>();
            characterView.Initialize(character);

            return character;
        }
    }
}
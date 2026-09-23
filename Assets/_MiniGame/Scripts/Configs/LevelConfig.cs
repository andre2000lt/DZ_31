using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _MiniGame
{
    [CreateAssetMenu(menuName = "Config/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public WinConditionType WinConditionType { get; private set; }
        [field: SerializeField]
        public LoseConditionType LoseConditionType { get; private set; }

        [field: SerializeField]
        public int EnemiesSpawnedToLose { get; private set; }
        [field: SerializeField]
        public int DefeatEnemiesToWin { get; private set; }
        [field: SerializeField]
        public int SecondsToSurvive { get; private set; }

        [field: SerializeField]
        public Vector3 HeroSpawnPoint { get; private set; }

        [field: SerializeField]
        public float EnemySpawnInterval { get; private set; }
        [field: SerializeField]
        public List<Vector3> EnemiesSpawnPoints { get; private set; }
        [field: SerializeField]
        public EnemyConfig EnemyConfig { get; private set; }

        [ContextMenu("UpdateHeroSpawnPoint")]
        public void UpdateHeroSpawnPoint()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("HeroSpawnPoint");
            HeroSpawnPoint = spawnPoint.transform.position;
        }

        [ContextMenu("UpdateEnemiesSpawnPoints")]
        public void UpdateEnemySpawnPoint()
        {
            List<GameObject> spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").ToList();

            foreach (GameObject spawnPoint in spawnPoints)
            {
                EnemiesSpawnPoints.Add(spawnPoint.transform.position);
            }
        }
    }
}
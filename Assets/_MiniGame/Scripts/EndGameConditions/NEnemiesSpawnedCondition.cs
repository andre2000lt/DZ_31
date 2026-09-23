using _Minigame;

namespace _MiniGame.Scripts.EndGameConditions
{
    public class NEnemiesSpawnedCondition : IEndGameCondition
    {
        private HeroSpawner _heroSpawner;
        private EnemiesSpawner _enemiesSpawner;
        private readonly LevelConfig _levelConfig;

        public NEnemiesSpawnedCondition(HeroSpawner heroSpawner, EnemiesSpawner enemiesSpawner, LevelConfig levelConfig)
        {
            _heroSpawner = heroSpawner;
            _enemiesSpawner = enemiesSpawner;
            _levelConfig = levelConfig;
        }

        public EndGameStatus GetEndGameStatus(float deltaTime)
        {
            if (_heroSpawner.Hero.IsDead) return EndGameStatus.GameLost;

            if (_enemiesSpawner.EnemyCount >= _levelConfig.EnemiesSpawnedToLose)
                return EndGameStatus.GameLost;

            return EndGameStatus.None;
        }
    }
}
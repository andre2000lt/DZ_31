using _Minigame;

namespace _MiniGame.Scripts.EndGameConditions
{
    public class EndGameConditionsFactory
    {
        private readonly HeroSpawner _heroSpawner;
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly LevelConfig _levelConfig;

        public EndGameConditionsFactory(HeroSpawner heroSpawner, EnemiesSpawner enemiesSpawner, LevelConfig levelConfig)
        {
            _heroSpawner = heroSpawner;
            _enemiesSpawner = enemiesSpawner;
            _levelConfig = levelConfig;
        }

        public NEnemiesSpawnedCondition CreateNEnemiesSpawnedCondition()
        {
            return new NEnemiesSpawnedCondition(_heroSpawner, _enemiesSpawner, _levelConfig);
        }

        public HeroDeadCondition CreateHeroDeadCondition()
        {
            return new HeroDeadCondition(_heroSpawner);
        }

        public SecondsToSurviveCondition CreateSecondsToSurviveCondition()
        {
            return new SecondsToSurviveCondition(_levelConfig);
        }

        public EnemiesDefeatedCondition CreateEnemiesDefeatedCondition()
        {
            return new EnemiesDefeatedCondition(_enemiesSpawner, _levelConfig);
        }
    }
}
using _Minigame;

namespace _MiniGame.Scripts.EndGameConditions
{
    public class EnemiesDefeatedCondition : IEndGameCondition
    {
        private readonly LevelConfig _levelConfig;
        private readonly EnemiesSpawner _enemiesSpawner;

        public EnemiesDefeatedCondition(EnemiesSpawner enemiesSpawner, LevelConfig levelConfig)
        {
            _enemiesSpawner = enemiesSpawner;
            _levelConfig = levelConfig;
        }

        public EndGameStatus GetEndGameStatus(float deltaTime)
        {
            if (_enemiesSpawner.DeadEnemyCount >= _levelConfig.DefeatEnemiesToWin)
            {
                return EndGameStatus.GameWon;
            }

            return EndGameStatus.None;
        }
    }
}
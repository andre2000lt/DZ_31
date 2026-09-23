namespace _MiniGame.Scripts.EndGameConditions
{
    public class EndGameChecker
    {
        private readonly IEndGameCondition _loseGameCondition;
        private readonly IEndGameCondition _winGameCondition;

        public EndGameChecker(LevelConfig levelConfig, EndGameConditionsFactory endGameConditionsFactory)
        {
            switch (levelConfig.LoseConditionType)
            {
                case LoseConditionType.NEnemiesSpawned:
                    _loseGameCondition = endGameConditionsFactory.CreateNEnemiesSpawnedCondition();
                    break;

                case LoseConditionType.HeroIsDead:
                    _loseGameCondition = endGameConditionsFactory.CreateHeroDeadCondition();
                    break;
            }

            switch (levelConfig.WinConditionType)
            {
                case WinConditionType.SurviveNSeconds:
                    _winGameCondition = endGameConditionsFactory.CreateSecondsToSurviveCondition();
                    break;

                case WinConditionType.DefeatNEnemies:
                    _winGameCondition = endGameConditionsFactory.CreateEnemiesDefeatedCondition();
                    break;
            }
        }

        public EndGameStatus GetStatus(float deltaTime)
        {
            EndGameStatus status = _loseGameCondition.GetEndGameStatus(deltaTime);

            if (status != EndGameStatus.None)
                return status;

            return _winGameCondition.GetEndGameStatus(deltaTime);
        }
    }
}
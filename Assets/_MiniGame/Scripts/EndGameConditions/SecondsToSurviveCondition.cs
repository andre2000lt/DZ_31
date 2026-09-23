namespace _MiniGame.Scripts.EndGameConditions
{
    public class SecondsToSurviveCondition : IEndGameCondition
    {
        private readonly LevelConfig _levelConfig;
        private float _secondsPassed;

        public SecondsToSurviveCondition(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public EndGameStatus GetEndGameStatus(float deltaTime)
        {
            _secondsPassed += deltaTime;

            if (_secondsPassed >= _levelConfig.SecondsToSurvive)
            {
                return EndGameStatus.GameWon;
            }

            return EndGameStatus.None;
        }
    }
}
namespace _MiniGame.Scripts.EndGameConditions
{
    public class HeroDeadCondition : IEndGameCondition
    {
        private HeroSpawner _heroSpawner;

        public HeroDeadCondition(HeroSpawner heroSpawner)
        {
            _heroSpawner = heroSpawner;
        }

        public EndGameStatus GetEndGameStatus(float deltaTime)
        {
            if (_heroSpawner.Hero.IsDead) return EndGameStatus.GameLost;

            return EndGameStatus.None;
        }
    }
}
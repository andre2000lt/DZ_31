namespace _MiniGame.Scripts.EndGameConditions
{
    public interface IEndGameCondition
    {
        EndGameStatus GetEndGameStatus(float deltaTime);
    }
}
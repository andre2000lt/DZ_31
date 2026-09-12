namespace _MiniGame
{
    public class ControllersFactory
    {
        public EnemyController CreateEnemyController(IMovable movable, float changeDirectionInterval)
        {
            return new EnemyController(movable, changeDirectionInterval);
        }

        public UserMoveController CreateUserMoveController(IMovable movable)
        {
            return new UserMoveController(movable);
        }
    }
}
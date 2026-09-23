namespace _MiniGame
{
    public class ControllersFactory
    {
        public RandomeMovementController CreateEnemyController(IMovable movable, float changeDirectionInterval)
        {
            return new RandomeMovementController(movable, changeDirectionInterval);
        }

        public UserMoveController CreateUserMoveController(IMovable movable)
        {
            return new UserMoveController(movable);
        }

        public UserShootController CreateShootController(IShooter shooter)
        {
            return new UserShootController(shooter);
        }

        public ComposireController CreateShootAndMoveController
        (
            UserShootController shootController,
            UserMoveController moveController
        )
        {
            return new ComposireController(moveController, shootController);
        }
    }
}
using UnityEngine;

namespace _MiniGame
{
    public class UserMoveController : Controller
    {
        private IMovable _movableUnit;

        public UserMoveController(IMovable movableUnit)
        {
            _movableUnit = movableUnit;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            float InputX = Input.GetAxisRaw("Horizontal");
            float InputZ = Input.GetAxisRaw("Vertical");

            Vector3 direction = new(InputX, 0f, InputZ);
            Vector3 destination = _movableUnit.Position + direction;

            _movableUnit.SetDestination(destination);
        }
    }
}
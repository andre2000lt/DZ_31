using UnityEngine;

namespace _MiniGame
{
    public class UserShootController : Controller
    {
        private IShooter _shooter;

        public UserShootController(IShooter shooter)
        {
            _shooter = shooter;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shooter.Shoot();
            }
        }
    }
}
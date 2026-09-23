using UnityEngine;

namespace _MiniGame
{
    public class RandomeMovementController : Controller
    {
        private float _changeDirectionInterval;
        private IMovable _movableUnit;
        private float timer;
        private Vector3 _direction = Vector3.zero;

        public RandomeMovementController(IMovable movableUnit, float changeDirectionInterval)
        {
            _movableUnit = movableUnit;
            _changeDirectionInterval = changeDirectionInterval;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_movableUnit.IsInitialize == false) return;

            timer += deltaTime;

            if (timer >= _changeDirectionInterval)
            {
                _direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

                timer = 0;
            }

            Vector3 offset = _direction.normalized * 0.5f;
            Vector3 destination = _movableUnit.Position + offset;

            _movableUnit.SetDestination(destination);
        }
    }
}
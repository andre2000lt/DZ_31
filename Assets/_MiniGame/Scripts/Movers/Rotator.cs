using UnityEngine;

namespace _MiniGame
{
    public class Rotator
    {
        private Transform _unitTransform;
        private float _rotationSpeed;

        private Vector3 _currentDirection;

        public Rotator(Transform unitTransform, float rotationSpeed)
        {
            _unitTransform = unitTransform;
            _rotationSpeed = rotationSpeed;
        }

        public void SetDirection(Vector3 direction)
        {
            _currentDirection = direction;
        }

        public void Update(float deltaTime)
        {
            if (_currentDirection.magnitude <= 0.05f) return;

            Quaternion rotation = Quaternion.LookRotation(_currentDirection);

            float step = deltaTime * _rotationSpeed;
            _unitTransform.rotation = Quaternion.RotateTowards(_unitTransform.rotation, rotation, step);
        }
    }
}
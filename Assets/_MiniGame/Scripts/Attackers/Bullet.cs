using UnityEngine;

namespace _MiniGame
{
    public class Bullet : MonoBehaviour
    {
        private const float DestroyAfterSeconds = 10f;
        private const float Speed = 3f;
        private const int Damage = 60;

        private Vector3 _direction;

        private void Update()
        {
            transform.Translate(_direction * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable unit))
            {
                unit.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }

        public void Initialize(Vector3 direction)
        {
            _direction = direction;

            Destroy(gameObject, DestroyAfterSeconds);
        }
    }
}
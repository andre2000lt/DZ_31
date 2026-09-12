using System;
using UnityEngine;
using UnityEngine.AI;

namespace _MiniGame
{
    public class Character : MonoBehaviour, IMovable, IDamageable
    {
        public event Action<Character> Dead;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotationSpeed;

        private ReactVariable<int> _health;

        private Rotator _rotator;

        public Vector3 Velocity => _agent.velocity;

        public Quaternion CurrentRotation => transform.rotation;

        public Vector3 Position => transform.position;
        public IReadOnlyReactVariable<int> Health => _health;
        public bool IsInitialize { get; private set; }
        public bool IsDead => _health.Value <= 0;

        public void Initialize(int healthAmount, Rotator rotator, float speed)
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.speed = speed;

            _rotator = rotator;

            _health = new ReactVariable<int>(healthAmount);

            IsInitialize = true;
        }

        private void Update()
        {
            if (IsDead) return;

            _rotator?.Update(Time.deltaTime);
        }

        public void SetDestination(Vector3 destination)
        {
            if (IsDead) return;

            _agent.SetDestination(destination);
            _rotator.SetDirection(_agent.velocity);
        }

        public void TakeDamage(int damage)
        {
            _health.Value -= damage;

            if (_health.Value <= 0)
            {
                Collider collider = GetComponent<Collider>();

                if (collider != null)
                    Destroy(collider);

                Dead?.Invoke(this);
            }
        }
    }
}
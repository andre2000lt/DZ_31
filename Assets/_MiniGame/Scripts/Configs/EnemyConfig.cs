using UnityEngine;

namespace _MiniGame
{
    [CreateAssetMenu(menuName = "Config/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField]
        public float RotationSpeed { get; private set; } = 700f;
        [field: SerializeField]
        public float Speed { get; private set; } = 2f;

        [SerializeField] private int _minHealth = 70;
        [SerializeField] private int _maxHealth = 100;

        [SerializeField] private float _minChangeDirectionInterval = 3f;
        [SerializeField] private float _maxChangeDirectionInterval = 7f;

        public int Health => Random.Range(_minHealth, _maxHealth + 1);
        public float ChangeDirectionInterval =>
            Random.Range(_minChangeDirectionInterval, _maxChangeDirectionInterval);
    }
}
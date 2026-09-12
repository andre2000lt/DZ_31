using UnityEngine;

namespace _MiniGame
{
    public class CharacterView : MonoBehaviour
    {
        private const float InjuredValue = 0.3f;
        private const int InjuredLayerIndex = 1;
        private readonly string IsWalkingKey = "IsWalking";
        private readonly string IsDeadKey = "IsDead";

        [SerializeField] private Animator _animator;

        private Character _character;

        private bool _isDead;

        public void Initialize(Character character)
        {
            _character = character;
            _character.Health.Changed += OnHealthChanged;
        }

        private void Update()
        {
            if (_isDead) return;
            if (_character == null) return;

            bool isWalking = _character.Velocity.magnitude > 0.05f;
            _animator.SetBool(IsWalkingKey, isWalking);
        }

        private void OnHealthChanged(int oldValue, int newValue, int maxValue)
        {
            float healthPercentage = (float)newValue / maxValue;
            bool isInjured = healthPercentage < InjuredValue;
            int weight = isInjured ? 1 : 0;
            _animator.SetLayerWeight(InjuredLayerIndex, weight);

            bool isDead = newValue <= 0;

            if (isDead)
            {
                _animator.SetBool(IsDeadKey, true);
                _isDead = true;
            }
        }
    }
}
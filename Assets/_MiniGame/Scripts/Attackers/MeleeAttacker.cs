using UnityEngine;

namespace _MiniGame
{
    public class MeleeAttacker : MonoBehaviour
    {
        private const int _damageAmount = 20;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable unit))
            {
                unit.TakeDamage(_damageAmount);
            }
        }
    }
}
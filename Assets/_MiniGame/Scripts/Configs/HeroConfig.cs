using UnityEngine;

namespace _MiniGame
{
    [CreateAssetMenu(menuName = "Config/HeroConfig", fileName = "HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField]
        public int HealthAmount { get; private set; }
        [field: SerializeField]
        public float RotationSpeed { get; private set; } = 700f;
        [field: SerializeField]
        public float Speed { get; private set; } = 2f;
    }
}
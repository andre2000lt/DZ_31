using System.Collections.Generic;
using UnityEngine;

namespace _MiniGame
{
    [CreateAssetMenu(menuName = "Config/LevelConfigCollection", fileName = "LevelConfogCollection")]
    public class LevelConfigCollection : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelConfigs;

        public LevelConfig GetRandom()
        {
            return _levelConfigs[Random.Range(0, _levelConfigs.Count)];
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Enemy.Configs
{

    [CreateAssetMenu(fileName = "WaveConfig", menuName = "WaveConfig")]
    public class RandomizedWaveConfig : ScriptableObject
    {
        [field: SerializeField] public List<RandomizedOneTypeEnemyListConfig> Enemies { get; private set; }
        [field: SerializeField] public RandomizedOneTypeEnemyListConfig Boss {get; private set; }
    }
}
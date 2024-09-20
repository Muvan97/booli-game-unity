using System.Collections.Generic;
using Systems.Enemy.Configs;
using UnityEngine;

namespace Systems.Level.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public float TimeBetweenSpawnNextEnemy { get; private set; }
        [field: SerializeField] public MapConfig Map { get; private set; }
        [field: SerializeField] public List<RandomizedWaveConfig> RandomWaveConfig { get; private set; }
    }
}
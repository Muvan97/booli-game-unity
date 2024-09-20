using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Enemy.Configs
{
    [Serializable]
    public class RandomizedOneTypeEnemyListConfig
    {
        [field: Range(1, 1000)] [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public List<EnemyConfig> Configs { get; private set; }
    }
}
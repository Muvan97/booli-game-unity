using System;
using UnityEngine;

namespace Systems.Enemy.Configs
{
    [Serializable]
    public class OneTypeEnemyList 
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public EnemyConfig Config { get; private set; }
    }
}
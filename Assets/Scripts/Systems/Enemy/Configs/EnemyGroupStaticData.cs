using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Enemy.Configs
{
    [Serializable]
    public class EnemyGroupStaticData
    {
        [field: SerializeField] public List<OneTypeEnemyList> Enemies { get; private set; }
    }
}
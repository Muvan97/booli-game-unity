using System;
using UnityEngine;

namespace Systems.Tower.Configs
{
    [Serializable]
    public class BomberTowerLevelConfig : TowerLevelConfig
    {
        [field: SerializeField] public float BombDamagingRadius { get; private set; }
    }
}
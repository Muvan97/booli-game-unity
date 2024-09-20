using System.Collections.Generic;
using Systems.Tower.Missile;
using UnityEngine;

namespace Systems.Tower.Configs
{
    [CreateAssetMenu(fileName = "NotBomberTowerConfig", menuName = "TowersConfigs/NotBomberTowerConfig")]
    public class NotBomberTowerConfig : TowerConfig
    {
        [field: SerializeField] public MissileConfig MissileConfig { get; private set; }
        [field: SerializeField] public List<TowerLevelConfig> TowerConfigs { get; private set; }
    }
}
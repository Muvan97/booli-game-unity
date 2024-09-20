using System.Collections.Generic;
using Systems.Tower.Missile;
using UnityEngine;

namespace Systems.Tower.Configs
{
    [CreateAssetMenu(fileName = "BomberTowerConfig", menuName = "TowersConfigs/BomberTowerConfig")]
    public class BomberTowerConfig : TowerConfig
    {
        [field: SerializeField] public BombMissileConfig MissileConfig { get; private set; }
        [field: SerializeField] public List<BomberTowerLevelConfig> TowerConfigs { get; private set; }
        [field: SerializeField] public float BombExplosionRadius { get; private set; }
    }
}
using System.Collections.Generic;
using Systems.Tower.Configs;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "TowersConfig", menuName = "TowersConfig")]
    public class TowersConfig : ScriptableObject
    {
        [field: SerializeField] public float UpgradeIncreasingPricePerLevelMultiplier { get; private set; } = 1.8f;
        [field: SerializeField] public float ReducingCooldownValuePerOpenedLevelConfig { get; private set; } = 0.05f;
        [field: SerializeField] public float AddingDamagePerLevel { get; private set; } = 5;
        [field: SerializeField] public List<TowerConfig> Configs { get; private set; }
    }
}
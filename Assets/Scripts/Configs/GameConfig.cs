using System.Collections.Generic;
using Systems.Tower.Configs;
using Systems.UI.BuildingTowerPopup.BuildingButton;
using Systems.UI.ShopPopup.CoinPurchaseButton;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public TonWalletConfig TonWalletConfig { get; private set; }
        [field: SerializeField] public RewardsForRefferalsConfig RewardsForRefferalsConfig { get; private set; }
        [field: SerializeField] public List<CoinPurchasingButtonConfig> CoinPurchaseButtonConfigs { get; private set; }
        [field: SerializeField] public List<BuildingButtonConfig> BuildingTowerConfigs { get; private set; }
        [field: SerializeField] public LevelsConfig LevelsConfig { get; private set; }
        [field: SerializeField] public TowersConfig TowersConfig { get; private set; }
        [field: SerializeField] public AnimationsConfigs AnimationsConfigs { get; private set; }
        [field: SerializeField] public List<AudioMixerConfig> AudioMixerConfigs { get; private set; }
        [field: SerializeField] public SoundsConfig SoundsConfig { get; private set; }
        [field: SerializeField] public int ShortNicknameLength { get; private set; } = 2;
        [field: SerializeField] public long PriceForUsingGameAcceleration { get; private set; }
        [field: SerializeField] public int StartBalance { get; private set; }
        //[field: SerializeField] public float TimeGameAcceleration { get; private set; }
        [field: SerializeField] public float GameAccelerationMultiplierWhenGameAccelerationActive { get; private set; } = 2;
        [field: SerializeField] public float GameAccelerationMultiplierWhenGameAccelerationInactive { get; private set; } = 1;
        [field: SerializeField] public LayerMask DiedEnemyLayerMask { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public LayerMask TowerPlaceLayerMask { get; private set; }
        [field: SerializeField] public SDK LoadSDKType { get; private set; }
    }
}
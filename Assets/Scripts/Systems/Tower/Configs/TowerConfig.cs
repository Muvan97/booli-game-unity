using UnityEngine;
using UnityEngine.Localization;

namespace Systems.Tower.Configs
{
    public abstract class TowerConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 TowerImageSizeInUpgradeTowerButtonView { get; private set; }
        [field: SerializeField] public Vector2 TowerImageOffsetInUpgradeTowerButtonView { get; private set; }
        [field: SerializeField] public bool IsHasAttackAnimation { get; private set; }
        [field: SerializeField] public float StartCooldown { get; private set; }
        [field: SerializeField] public float StartDamage { get; private set; }
        [field: SerializeField] public int TowerIndex { get; private set; }
        [field: SerializeField] public int StartUpgradingPrice { get; private set; }
        [field: SerializeField] public LocalizedString LocalizedName { get; private set; }
        [field: SerializeField] public TowerComponents TowerPrefab { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public float ShootingRadius { get; private set; }
    }
}
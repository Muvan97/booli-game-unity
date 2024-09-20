using System;
using UnityEngine;

namespace Systems.Tower.Configs
{
    [Serializable]
    public class TowerLevelConfig : IComparable<TowerLevelConfig>
    {
        //[field: SerializeField] public int Damage { get; private set; }
        //[field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public int MinimumLevel { get; private set; }
        [field: SerializeField] public Sprite TowerSprite { get; private set; }
        [field: SerializeField] public AnimatorOverrideController AnimatorOverrideController { get; private set; }

        public int CompareTo(TowerLevelConfig other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return Convert.ToInt16(MinimumLevel > other.MinimumLevel);
        }
    }
}
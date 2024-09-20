using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Systems.UI.UpgradeTowerPopup.UpgradeTowerButton
{
    [Serializable]
    public class UpgradeTowerData : IEquatable<UpgradeTowerData>
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public int TowerIndex { get; private set; }

        public UpgradeTowerData(int index) => TowerIndex = index;
        
        [JsonConstructor]
        public UpgradeTowerData(int towerIndex, int level)
        {
            Level = level;
            TowerIndex = towerIndex;
        }

        public bool IsPurchased() => Level > 0;
        public void IncreaseLevel() => Level++;

        public bool Equals(UpgradeTowerData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            return TowerIndex == other.TowerIndex;
        }

        public override int GetHashCode() => 0;
    }
}
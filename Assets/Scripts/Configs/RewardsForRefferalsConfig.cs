using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class RewardsForRefferalsConfig
    {
        [field: SerializeField] [field: Min(1)] public int BonusInCoins { get; private set; }
        [field: SerializeField] [field: Min(1)] public int BonusInBooli { get; private set; }
        [field: SerializeField] [field: Range(1, 100)] public int NumberOpenedLevelsForGiveBonusForRefferal { get; private set; }

        public List<int> Bonuses => new List<int> {BonusInBooli, BonusInCoins};
    }
}
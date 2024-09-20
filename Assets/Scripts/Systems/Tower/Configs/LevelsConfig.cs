using System;
using System.Collections.Generic;
using Systems.Level.Configs;
using UnityEngine;

namespace Systems.Tower.Configs
{
    [Serializable]
    public class LevelsConfig
    {
        [field: SerializeField] public float TimeAfterWinBeforeOpenMainMenu { get; private set; }
        [field: SerializeField] public float TimeAfterKillBossBeforeShowWinPopup { get; private set; } = 2;
        [field: SerializeField] public float TimeBeforeStartFirstWave { get; private set; } = 5;
        [field: SerializeField] public int NumberEnemiesWhoEndedPathBeforeShowDefeatPopup { get; private set; }
        [field: SerializeField] public int NumberLifeWhichBossTakesAway { get; private set; } = 5;
        [field: SerializeField] public int NumberLifeWhichNotBossTakesAway { get; private set; } = 1;
        [field: SerializeField] public float EnemyHealthMultiplierPerWave { get; private set; }  = 1.2f;
        [field: SerializeField] public float BossHealthMultiplierPerWave { get; private set; }  = 1.5f;
        [field: SerializeField] public float RewardForKillEnemyMultiplierPerWave { get; private set; }  = 1;
        [field: SerializeField] public List<LevelConfig> LevelConfigs { get; private set; }
    }
}
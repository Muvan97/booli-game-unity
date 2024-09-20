using System.Collections.Generic;
using System.Threading;
using Systems.Enemy.Configs;
using Systems.Level.Configs;
using Systems.Level.LevelPopup;
using Configs;
using Infrastructure.EventBusSystem;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.States;
using Logic.Other;
using UnityEngine;
using Timer = Logic.Other.Timer;

namespace Systems.Level.LevelWaves
{
    public class LevelWavesModel
    {
        public Timer TimerBetweenCreateNextEnemy { get; private set; }
        public int RemainingEnemiesOnWave { get; private set; }
        public RandomizedWaveConfig CurrentWave => _levelConfig.RandomWaveConfig[_currentWaveIndex];
        public float TimeBeforeSpawnNextEnemy => _levelConfig.TimeBetweenSpawnNextEnemy;
        public readonly Counter LifeCounter;
        public readonly CurrenciesProviderService CurrenciesProviderService;
        public readonly CancellationToken DestroyCancellationToken;
        public readonly List<Way> Ways;
        public readonly IGameFactory GameFactory;
        public readonly GameStateMachine GameStateMachine;
        public readonly GameConfig GameConfig;
        public readonly GameDataProviderAndSaverService GameDataProviderAndSaverService;
        public readonly IEventBus EventBus;

        private readonly LevelConfig _levelConfig;
        public readonly IUIFactory UIFactory;
        private int _currentWaveIndex;

        public LevelWavesModel(LevelConfig levelConfig,
            LevelPopupView levelPopupView, List<Way> ways, IGameFactory gameFactory, GameStateMachine gameStateMachine,
            GameConfig gameConfig, IUIFactory uiFactory,
            GameDataProviderAndSaverService gameDataProviderAndSaverService,
            CurrenciesProviderService currenciesProviderService, IEventBus eventBus, Counter lifeCounter)
        {
            _levelConfig = levelConfig;
            DestroyCancellationToken = levelPopupView.destroyCancellationToken;
            TimerBetweenCreateNextEnemy = new Timer(DestroyCancellationToken);
            Ways = ways;
            GameFactory = gameFactory;
            GameStateMachine = gameStateMachine;
            GameConfig = gameConfig;
            UIFactory = uiFactory;
            GameDataProviderAndSaverService = gameDataProviderAndSaverService;
            CurrenciesProviderService = currenciesProviderService;
            EventBus = eventBus;
            LifeCounter = lifeCounter;
        }

        public long GetRewardForKillEnemyByFormula(EnemyConfig enemyConfig)
        {
            var multiplierPerWave = GameConfig.LevelsConfig.RewardForKillEnemyMultiplierPerWave;
            
            return enemyConfig.RewardForKill + (long)(GameDataProviderAndSaverService.GameData.OpenLevelIndex *
                multiplierPerWave);
        }

        public bool IsLastWave() => _levelConfig.RandomWaveConfig.Count <= _currentWaveIndex + 1;

        public void ReduceRemainingEnemiesOnWave() => RemainingEnemiesOnWave--;

        public void TryIncreaseCurrentWaveIndex()
        {
            if (!IsLastWave())
                _currentWaveIndex++;
        }

        public void FillRemainingEnemiesOnWave()
        {
            _levelConfig.RandomWaveConfig[_currentWaveIndex].Enemies
                .ForEach(oneTypeEnemy => RemainingEnemiesOnWave += oneTypeEnemy.Number);
            RemainingEnemiesOnWave++; // boss
        }
    }
}
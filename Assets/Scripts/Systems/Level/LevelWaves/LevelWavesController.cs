using System;
using System.Threading;
using Systems.Enemy.Configs;
using Systems.Level.GameAcceleration;
using Systems.Level.LevelPopup;
using Configs;
using Cysharp.Threading.Tasks;
using Infrastructure.States;
using Tools;
using UnityEngine;
using Timer = Logic.Other.Timer;

namespace Systems.Level.LevelWaves
{
    public class LevelWavesController
    {
        private readonly LevelWavesModel _wavesModel;
        private readonly LevelPopupView _view;
        private readonly GameAccelerationModel _gameAccelerationModel;

        public LevelWavesController(LevelWavesModel wavesModel, LevelPopupView levelPopupView, GameAccelerationModel gameAccelerationModel)
        {
            _wavesModel = wavesModel;
            _view = levelPopupView;
            _gameAccelerationModel = gameAccelerationModel;
            
            _wavesModel.TimerBetweenCreateNextEnemy.Set(_wavesModel.TimeBeforeSpawnNextEnemy / 
                                                        _wavesModel.GameConfig.GameAccelerationMultiplierWhenGameAccelerationInactive);
            
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            Subscribe();

            _wavesModel.Ways.ForEach(way => way.CalculateLength());
            _wavesModel.FillRemainingEnemiesOnWave();
            UpdateRemainingEnemyCounterText();
            _view.GameAccelerationButton.interactable = false;

            await UniTask.Delay(TimeSpan.FromSeconds(_wavesModel.GameConfig.LevelsConfig.TimeBeforeStartFirstWave), 
                cancellationToken: _view.destroyCancellationToken);
            
            _view.GameAccelerationButton.interactable = true;
            
            StartWave();
        }

        private void Subscribe()
        {
            _wavesModel.GameFactory.EnemyEndedPath += ReduceEnemyFromCounter;
            _wavesModel.GameFactory.EnemyEndedPath += DoIfRemainingEnemyNumberZero;
            _view.DestroyReporter.Destroyed += Unsubscribe;
            _wavesModel.EventBus.Subscribe<GameAccelerationChangedEvent>(changedEvent => 
                _wavesModel.TimerBetweenCreateNextEnemy.SetCountingTimeWithoutRestart(
                    _wavesModel.TimeBeforeSpawnNextEnemy / changedEvent.AccelerationMultiplier));
        }

        private void Unsubscribe() => _wavesModel.GameFactory.EnemyEndedPath -= ReduceEnemyFromCounter;

        private void StartWave() => InstantiateEnemies().Forget();

        private async UniTask InstantiateEnemies()
        {
            foreach (var oneTypeEnemyList in _wavesModel.CurrentWave.Enemies)
            {
                var randomEnemyConfig = oneTypeEnemyList.Configs.GetRandomElement();

                for (var i = 0; i < oneTypeEnemyList.Number; i++)
                {
                    InstantiateAndSubscribeToEnemy(randomEnemyConfig);
                    _wavesModel.TimerBetweenCreateNextEnemy.StartCountingTime();
                    await UniTask.WaitWhile(() => _wavesModel.TimerBetweenCreateNextEnemy.IsCounting(),
                        cancellationToken: _wavesModel.DestroyCancellationToken);
                }
            }

            await UniTask.WaitWhile(() => _wavesModel.TimerBetweenCreateNextEnemy.IsCounting(),
                cancellationToken: _wavesModel.DestroyCancellationToken);
            
            InstantiateAndSubscribeToEnemy(_wavesModel.CurrentWave.Boss.Configs.GetRandomElement(), true);
        }

        private void InstantiateAndSubscribeToEnemy(EnemyConfig randomEnemyConfig, bool isBoss = false)
        {
            var enemy = _wavesModel.GameFactory.CreateEnemy(randomEnemyConfig, _wavesModel.Ways, _gameAccelerationModel, isBoss);
            enemy.EntityHealthController.Model.Died += () => OnDieEnemy(randomEnemyConfig, enemy.transform.position, isBoss);
        }

        private void DoIfRemainingEnemyNumberZero()
        {
            if (_wavesModel.RemainingEnemiesOnWave != 0 || _wavesModel.LifeCounter.Count == 0) return;

            if (_wavesModel.IsLastWave())
            {
                if (_wavesModel.GameDataProviderAndSaverService.GameData.OpenLevelIndex + 1 < _wavesModel.GameConfig.LevelsConfig.LevelConfigs.Count)
                    _wavesModel.GameDataProviderAndSaverService.GameData.OpenLevelIndex++;

                _wavesModel.GameDataProviderAndSaverService.SaveData();
                
                Timer.StartCountingTime(CancellationToken.None, _wavesModel.GameConfig.LevelsConfig.TimeAfterKillBossBeforeShowWinPopup,
                    _wavesModel.UIFactory.GetWinPopup().OpenPopup);
                
                Timer.StartCountingTime(CancellationToken.None, _wavesModel.GameConfig.LevelsConfig.TimeAfterWinBeforeOpenMainMenu,
                    _wavesModel.GameStateMachine.Enter<MainMenuState>);
                _wavesModel.GameDataProviderAndSaverService.SaveData();
            }
            else
            {
                _wavesModel.TryIncreaseCurrentWaveIndex();
                _view.RemainingEnemyCounterText.text = _wavesModel.RemainingEnemiesOnWave.ToString();
                StartWave();
            }
        }

        private void OnDieBoss()
        {
            
        }

        private void OnDieEnemy(EnemyConfig enemyConfig, Vector2 enemyPosition, bool isBoss)
        {
            ReduceEnemyFromCounter();

            var reward =  !isBoss
                ? _wavesModel.GetRewardForKillEnemyByFormula(enemyConfig) 
                : enemyConfig.RewardForKill;
            
            switch (enemyConfig.CurrencyForKill)
            {
                case Currency.Booli:
                    _wavesModel.CurrenciesProviderService.Booli.Number += reward;
                    CreateAndInitializeTextOverEnemy(enemyConfig, enemyPosition);
                    break;
                case Currency.Coin:
                    _wavesModel.CurrenciesProviderService.Coins.Number += reward;
                    break;
            }

            DoIfRemainingEnemyNumberZero();
        }

        private void CreateAndInitializeTextOverEnemy(EnemyConfig enemyConfig, Vector2 enemyPosition)
        {
            var textOverEnemy = _wavesModel.UIFactory.GetTextOverEnemy(
                enemyPosition);

            textOverEnemy.text = $"+{enemyConfig.RewardForKill}<sprite=0>";

            new TextTakingOffAnimation(textOverEnemy,
                _wavesModel.GameConfig.AnimationsConfigs.EarnedBooliTextAnimation);
        }

        private void ReduceEnemyFromCounter()
        {
            _wavesModel.ReduceRemainingEnemiesOnWave();
            UpdateRemainingEnemyCounterText();
        }

        private void UpdateRemainingEnemyCounterText() =>
            _view.RemainingEnemyCounterText.text = _wavesModel.RemainingEnemiesOnWave.ToString();
    }
}
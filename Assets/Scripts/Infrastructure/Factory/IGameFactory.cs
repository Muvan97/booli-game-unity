using System;
using System.Collections.Generic;
using Systems.Enemy;
using Systems.Enemy.Configs;
using Systems.EntityHealth;
using Systems.Level;
using Systems.Level.Configs;
using Systems.Level.GameAcceleration;
using Systems.Level.LevelWaves;
using Systems.Tower;
using Systems.Tower.Configs;
using Systems.Tower.Missile;
using Systems.Tower.Missile.Animation;
using Systems.Tower.Missile.Attacking;
using Infrastructure.Services.SaveLoad;
using Infrastructure.States;
using Logic.Observers;
using Logic.Other;
using Logic.Sound;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        Action EnemyEndedPath { get; set; }
        Action<EnemyComponents> CertainEnemyEndedPath { get; set; }
        void CreateOrderSortingByPositionYController();
        ButtonsClickSoundAudioSource CreateButtonsClickSoundSource();
        BackgroundAudioPlayer CreateBackgroundAudioPlayer();
        MapPrefab CreateMap(MapConfig mapConfig);
        MonoBehaviourObserver CreateMonoBehaviourObserver();
        TouchObserver2D CreateTouchObserver();
        TelegramDataObserver FindTelegramDataObserver();

        EnemyComponents CreateEnemy(EnemyConfig enemyConfig, List<Way> ways, GameAccelerationModel gameAccelerationModel,
            bool isBoss = false);
        
        void ClearActions();
        TowerComponents CreateTower(TowerConfig towerConfig, TowerLevelConfig towerLevelConfig, Vector3 position);

        void ConstructTower(TowerComponents instance, TowerConfig towerConfig,
            GameAccelerationModel gameAccelerationModel, int towerLevel);

        MissileView InstantiateMissileAndStartAnimation(
            MissileConfig missileConfig,
            Vector3 spawnPosition, EntityHealthController goal,
            GameAccelerationModel model, out IMissileAttackingController missileAttackingController,
            out IMissileFlyingAnimation missileFlyingAnimation, TowerConfig towerConfig,
            int towerLevel);
    }
}
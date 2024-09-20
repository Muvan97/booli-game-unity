using System.Threading;
using Systems.EntityHealth;
using Systems.Level.GameAcceleration;
using Systems.Tower.Animation;
using Systems.Tower.Configs;
using Systems.Tower.Missile;
using Configs;
using Infrastructure.Factory;
using Logic.Observers;
using Tools;
using UnityEngine;
using Timer = Logic.Other.Timer;

namespace Systems.Tower.Attacking
{
    public class TowerAttackingRealtimeModel : TowerAttackingModel
    {
        public EntityHealthController Goal
        {
            get
            {
                if (_goal?.View && _goal.View.transform == CircleObserver.NearestTransform)
                    return _goal;

                return _goal = CircleObserver.NearestTransform?.GetComponent<EntityHealthView>().Controller;
            }
        }

        public bool IsCanAttackWithoutCooldown { get; private set; }

        public bool IsEndPastAttackAnimation = true;
        public readonly int Level;
        public readonly MissileConfig MissileConfig;
        public readonly OverlapCircleObserver CircleObserver;
        public readonly Timer CooldownTimer;
        public readonly TowerConfig TowerConfig;
        public readonly IGameFactory GameFactory;
        public readonly GameAccelerationModel GameAccelerationModel;
        public readonly CancellationToken CancellationToken;

        private EntityHealthController _goal;
        private TowerLevelConfig _towerLevelConfig;

        public TowerAttackingRealtimeModel(TowerConfig towerConfig, TowerLevelConfig towerLevelConfig,
            TowerAttackingView view, OverlapCircleObserver circleObserver, IGameFactory gameFactory,
            MissileConfig missileConfig, GameAccelerationModel gameAccelerationModel,
            GameConfig gameConfig, int level)
        {
            CircleObserver = circleObserver;
            GameFactory = gameFactory;
            MissileConfig = missileConfig;
            GameAccelerationModel = gameAccelerationModel;
            Level = level;
            TowerConfig = towerConfig;
            CancellationToken = view.destroyCancellationToken;
            CooldownTimer = new Timer(CancellationToken);
            var cooldown = TowerStatsTools.GetCooldown(towerConfig.GetTowerLevelConfigs(),
                level, towerConfig.StartCooldown, gameConfig.TowersConfig.ReducingCooldownValuePerOpenedLevelConfig);
            
            IsCanAttackWithoutCooldown = cooldown == 0 && towerConfig.IsHasAttackAnimation;

            Debug.Assert(cooldown > 0 || towerConfig.IsHasAttackAnimation,
                $"Cooldown equal zero, but {towerConfig.name} haven't attack animation. " +
                $"Increase cooldown or make true is has attack animation...");
        }
        
        public bool IsCanInvokeAttacked() => Goal != null && !CooldownTimer.IsCounting()
        && (IsEndPastAttackAnimation || !TowerConfig.IsHasAttackAnimation);

        public bool IsEndAttackingAnimation(TowerAnimation towerAnimation) =>
            towerAnimation == TowerAnimation.Attacking;
    }
}
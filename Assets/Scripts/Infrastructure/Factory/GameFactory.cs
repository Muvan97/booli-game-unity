using System;
using System.Collections.Generic;
using Systems.Enemy;
using Systems.Enemy.Configs;
using Systems.Enemy.EnemyAnimatorController;
using Systems.Enemy.EnemyMovingAndRotation;
using Systems.EntityHealth;
using Systems.Level;
using Systems.Level.Configs;
using Systems.Level.GameAcceleration;
using Systems.OrderSortingByPositionY;
using Systems.Tower;
using Systems.Tower.Animation;
using Systems.Tower.Attacking;
using Systems.Tower.Configs;
using Systems.Tower.Missile;
using Systems.Tower.Missile.Animation;
using Systems.Tower.Missile.Attacking;
using Systems.Tower.TowerPlace;
using Configs;
using Infrastructure.AssetManagement;
using Infrastructure.EventBusSystem;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using Logic.Observers;
using Logic.Other;
using Logic.Sound;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public Action EnemyEndedPath { get; set; }
        public Action<EnemyComponents> CertainEnemyEndedPath { get; set; }

        private readonly IInputService _inputService;
        private readonly IStaticDataProviderService _staticDataProviderService;
        private readonly GameDataProviderAndSaverService _gameDataProviderAndSaverService;
        private readonly FactoriesContainerProviderService _factoriesContainerProviderService;
        private readonly IEventBus _eventBus;

        private ButtonsClickSoundAudioSource _buttonsClickSoundSource;

        private Action<Transform> _enemyInstantiated,
            _enemyDestroyed,
            _towerInstantiated,
            _missileInstantiated,
            _missileDestroyed;

        public GameFactory(IInputService inputService, IStaticDataProviderService staticDataProviderService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService, IEventBus eventBus,
            FactoriesContainerProviderService factoriesContainerProviderService)
        {
            _inputService = inputService;
            _staticDataProviderService = staticDataProviderService;
            _gameDataProviderAndSaverService = gameDataProviderAndSaverService;
            _eventBus = eventBus;
            _factoriesContainerProviderService = factoriesContainerProviderService;
        }

        public void ClearActions()
        {
            _enemyDestroyed = null;
            _enemyInstantiated = null;
            _missileInstantiated = null;
            _missileDestroyed = null;
            EnemyEndedPath = null;
            _towerInstantiated = null;
            CertainEnemyEndedPath = null;
        }

        public MapPrefab CreateMap(MapConfig mapConfig)
        {
            var instance = Object.Instantiate(mapConfig.Prefab);

            instance.TowersPlacesPoints.ForEach(config =>
                config.Point.Construct(new TowerPlaceModel()));

            return instance;
        }

        public BackgroundAudioPlayer CreateBackgroundAudioPlayer()
        {
            var backgroundAudioPlayer = _factoriesContainerProviderService.GetOrLoadAndRegisterMonoBehaviour
                <BackgroundAudioPlayer>(AssetPath.BackgroundAudioPlayer, out var isLoaded);

            if (isLoaded)
            {
                Object.DontDestroyOnLoad(backgroundAudioPlayer);
                backgroundAudioPlayer.ConstructAndStartPlaySounds(_staticDataProviderService.GameConfig.SoundsConfig
                    .MainMenuMusicClips);
            }

            return backgroundAudioPlayer;
        }

        public EnemyComponents CreateEnemy(EnemyConfig enemyConfig, List<Way> ways,
            GameAccelerationModel gameAccelerationModel, bool isBoss = false)
        {
            var targetPosition = new Vector3(ways[0].Points[0].position.x, ways[0].Points[0].position.y);
            var instance = Object.Instantiate(enemyConfig.Prefab, targetPosition, Quaternion.identity);
            instance.IsBoss = isBoss;

            InitializeEnemyHealth(enemyConfig, isBoss, instance, out var enemyHealthModel);
            InitializeEnemyMovingAndRotation(enemyConfig, ways, gameAccelerationModel, instance, enemyHealthModel,
                out var enemyMovingModel);

            var enemyAnimatorModel = new EnemyAnimatorModel();
            var enemyAnimatorController = new EnemyAnimatorController(enemyAnimatorModel, instance.AnimatorView,
                enemyHealthModel, enemyMovingModel);

            enemyAnimatorController.ChangeAnimationSpeedMultiplier(gameAccelerationModel.GameAccelerationMultiplier);
            _eventBus.Subscribe<GameAccelerationChangedEvent>(speed =>
                enemyAnimatorController.ChangeAnimationSpeedMultiplier(speed.AccelerationMultiplier));

            SubscribeToDieEnemy(enemyConfig, gameAccelerationModel, enemyHealthModel, instance);
            
            instance.DestroyReporter.Destroyed += () => _enemyDestroyed?.Invoke(instance.transform);
            _enemyInstantiated?.Invoke(instance.transform);

            return instance;
        }

        private void InitializeEnemyHealth(EnemyConfig enemyConfig, bool isBoss, EnemyComponents instance,
            out EntityHealthModel healthModel)
        {
            healthModel = new EntityHealthModel(GetEnemyHealthByFormula(enemyConfig, isBoss));
            var enemyHealthController = new EntityHealthController(healthModel, instance.HealthView);
            instance.HealthView.Construct(enemyHealthController);
            healthModel.Died += () =>
                instance.AudioSource.PlayOneShot(_staticDataProviderService.GameConfig.SoundsConfig.EnemyDeathSound);

            var healthSliderView = instance.HealthSliderView;
            var healthSliderController = new HealthSliderController(healthModel, healthSliderView);
        }

        private void InitializeEnemyMovingAndRotation(EnemyConfig enemyConfig, List<Way> ways,
            GameAccelerationModel gameAccelerationModel, EnemyComponents instance, EntityHealthModel enemyHealthModel,
            out EnemyMovingAndRotationModel model)
        {
            model = new EnemyMovingAndRotationModel(enemyConfig, gameAccelerationModel, ways);
            instance.Observer.FixedUpdated += model.UpdateCurrentT;
            var enemyRotationController =
                new EnemyRotationController(model, instance.transform, instance.Observer);
            var enemyMovingController =
                new EnemyMovingController(model, instance.Observer, enemyHealthModel);

            model.EndedPath += () => CertainEnemyEndedPath?.Invoke(instance);
            model.EndedPath += () => EnemyEndedPath?.Invoke();
            model.EndedPath += () => Object.Destroy(instance.gameObject);
        }

        private void SubscribeToDieEnemy(EnemyConfig enemyConfig, GameAccelerationModel gameAccelerationModel,
            EntityHealthModel enemyHealthModel, EnemyComponents instance)
        {
            enemyHealthModel.Died += () =>
                Object.Destroy(instance.gameObject,
                    enemyConfig.TimeBeforeDestroyAfterDead / gameAccelerationModel.GameAccelerationMultiplier);

            enemyHealthModel.Died += () =>
                instance.gameObject.layer =
                    PhysicsTools.GetLayerFromLayerMask(_staticDataProviderService.GameConfig.DiedEnemyLayerMask);
        }

        private float GetEnemyHealthByFormula(EnemyConfig enemyConfig, bool isBoss)
        {
            if (_gameDataProviderAndSaverService.GameData.OpenLevelIndex == 0)
                return enemyConfig.Health;

            var levelsConfig = _staticDataProviderService.GameConfig.LevelsConfig;

            var multiplierPerWave = isBoss
                ? levelsConfig.BossHealthMultiplierPerWave
                : levelsConfig.EnemyHealthMultiplierPerWave;

            return enemyConfig.Health * multiplierPerWave *
                   _gameDataProviderAndSaverService.GameData.OpenLevelIndex;
        }

        public TouchObserver2D CreateTouchObserver()
        {
            if (!_factoriesContainerProviderService.Container.IsExists<TouchObserver2D>() ||
                !_factoriesContainerProviderService.Container.GetSingle<TouchObserver2D>())
                _factoriesContainerProviderService.Container.RegisterSingle(
                    new GameObject("ClickObserver2D").AddComponent<TouchObserver2D>());

            return _factoriesContainerProviderService.Container.GetSingle<TouchObserver2D>();
        }

        public MonoBehaviourObserver CreateMonoBehaviourObserver() =>
            new GameObject().AddComponent<MonoBehaviourObserver>();

        public void CreateOrderSortingByPositionYController()
        {
            var orderSortingByPositionYModel = new OrderSortingByPositionYModel();
            var orderSortingByPositionYController = new OrderSortingByPositionYController(
                CreateMonoBehaviourObserver(), orderSortingByPositionYModel);

            _towerInstantiated += transform => orderSortingByPositionYModel.Transforms.Add(transform);
            _enemyInstantiated += transform => orderSortingByPositionYModel.Transforms.Add(transform);
            _enemyDestroyed += transform => orderSortingByPositionYModel.Transforms.Remove(transform);
            _missileDestroyed += transform => orderSortingByPositionYModel.Transforms.Remove(transform);
            _missileInstantiated += transform => orderSortingByPositionYModel.Transforms.Add(transform);
        }

        public ButtonsClickSoundAudioSource CreateButtonsClickSoundSource()
        {
            if (!_buttonsClickSoundSource)
            {
                _buttonsClickSoundSource =
                    AssetProvider.Instantiate<ButtonsClickSoundAudioSource>(AssetPath.ButtonsClickSoundSource);
                _buttonsClickSoundSource.Construct(_staticDataProviderService.GameConfig);
                Object.DontDestroyOnLoad(_buttonsClickSoundSource);
            }

            return _buttonsClickSoundSource;
        }

        public MissileView InstantiateMissileAndStartAnimation(MissileConfig missileConfig,
            Vector3 spawnPosition, EntityHealthController goal,
            GameAccelerationModel model, out IMissileAttackingController missileAttackingController,
            out IMissileFlyingAnimation missileFlyingAnimation, TowerConfig towerConfig,
            int towerLevel)
        {
            var instance = Object.Instantiate(missileConfig.MissilePrefab, spawnPosition, Quaternion.identity);
            var goalPosition = goal.View.transform.position;

            switch (missileConfig)
            {
                case BombMissileConfig bombMissileAnimationConfig:
                    CreateBombMissileAnimation(missileConfig, spawnPosition, model,
                        out missileFlyingAnimation, bombMissileAnimationConfig, goalPosition, instance);
                    break;
                default:
                    missileFlyingAnimation =
                        new MissileFlyingAnimation(missileConfig, goalPosition, instance.transform);
                    break;
            }

            if (towerConfig is BomberTowerConfig bomberTowerConfig)
            {
                CreateBombMissileModel(missileConfig,
                    out missileAttackingController, missileFlyingAnimation, goalPosition, towerLevel, bomberTowerConfig,
                    instance);
            }
            else
            {
                CreateMissileModel(goal, out missileAttackingController, missileFlyingAnimation, instance,
                    towerLevel, towerConfig, missileConfig);
            }

            _missileInstantiated?.Invoke(instance.transform);
            
            missileFlyingAnimation.StartAnimation(model.GameAccelerationMultiplier);

            return instance;
        }

        private void CreateBombMissileModel(MissileConfig missileConfig,
            out IMissileAttackingController missileAttackingController, IMissileFlyingAnimation missileFlyingAnimation,
            Vector3 goalPosition, int level, BomberTowerConfig towerConfig, MissileView instance)
        {
            var damage = TowerStatsTools.GetDamage(level, towerConfig.StartDamage,
                _staticDataProviderService.GameConfig.TowersConfig.AddingDamagePerLevel);

            var model = new BombMissileAttackingModel(damage, goalPosition,
                _staticDataProviderService.GameConfig.EnemyLayerMask, towerConfig.BombExplosionRadius);
            missileAttackingController = new BombMissileAttackingController(model);

            if (!(instance is BombMissileView bombView) ||
                !(missileConfig is BombMissileConfig bombMissileAnimationConfig)) return;

            missileFlyingAnimation.AnimationEnded +=
                () => bombView.Animator.SetTrigger(model.ExplodeTriggerHash);

            missileFlyingAnimation.AnimationEnded += () => Timer.StartCountingTime(instance.destroyCancellationToken,
                bombMissileAnimationConfig.TimeAfterExplosionStartBeforeDestroy,
                () => DestroyMissile(instance.transform));

            missileFlyingAnimation.AnimationEnded += () => instance.AudioSource.PlayOneShot(missileConfig.Sound);
        }

        private void CreateBombMissileAnimation(MissileConfig missileConfig, Vector3 spawnPosition,
            GameAccelerationModel model, out IMissileFlyingAnimation missileFlyingAnimation,
            BombMissileConfig bombMissileConfig, Vector3 goalPosition, MissileView instance)
        {
            var bombAnimation = new BombAnimation(bombMissileConfig, goalPosition,
                spawnPosition, instance.transform, model.GameAccelerationMultiplier);
            missileFlyingAnimation = bombAnimation;
            _eventBus.Subscribe<GameAccelerationChangedEvent>(changedEvent =>
                bombAnimation.Timer.SetCountingTimeWithoutRestart(missileConfig.FlyingTime /
                                                                  changedEvent.AccelerationMultiplier));
        }

        private void CreateMissileModel(EntityHealthController goal,
            out IMissileAttackingController missileAttackingController, IMissileFlyingAnimation missileFlyingAnimation,
            MissileView instance, int level, TowerConfig towerConfig, MissileConfig missileConfig)
        {
            var damage = TowerStatsTools.GetDamage(level, towerConfig.StartDamage,
                _staticDataProviderService.GameConfig.TowersConfig.AddingDamagePerLevel);

            var model = new NotBombMissileAttackingModel(damage, goal);
            missileAttackingController = new NotBombAttackingController(model);
            missileFlyingAnimation.AnimationEnded += () => DestroyMissile(instance.transform);
            instance.AudioSource.PlayOneShot(missileConfig.Sound);
        }

        private void DestroyMissile(Transform missile)
        {
            _missileDestroyed?.Invoke(missile);
            Object.Destroy(missile.gameObject);
        }

        public TelegramDataObserver FindTelegramDataObserver() => Object.FindObjectOfType<TelegramDataObserver>();

        public void ConstructTower(TowerComponents instance, TowerConfig towerConfig,
            GameAccelerationModel gameAccelerationModel, int towerLevel)
        {
            instance.SpriteRenderer.color = Color.white;

            MissileConfig missileConfig = null;
            var levelConfig = GetLevelConfig(towerConfig, towerLevel, ref missileConfig);

            var circleObserver = instance.gameObject.AddComponent<OverlapCircleObserver>();
            circleObserver.Construct(_staticDataProviderService.GameConfig.EnemyLayerMask, towerConfig.ShootingRadius);

            var animatorView = instance.AnimatorView;

            var animatorModel = new TowerAnimatorModel();
            var attackView = instance.AttackingView;

            var attackModel = new TowerAttackingRealtimeModel(towerConfig, levelConfig, attackView, circleObserver,
                this, missileConfig, gameAccelerationModel, _staticDataProviderService.GameConfig, towerLevel);
            var attackingController =
                new TowerAttackingController(attackModel, attackView, animatorView, animatorModel);

            var cooldown = TowerStatsTools.GetCooldown(towerConfig.GetTowerLevelConfigs(),
                towerLevel, towerConfig.StartCooldown,
                _staticDataProviderService.GameConfig.TowersConfig.ReducingCooldownValuePerOpenedLevelConfig);

            var towerAnimatorController = new TowerAnimatorController(animatorView, animatorModel, attackModel);

            towerAnimatorController.ChangeAnimationSpeedMultiplier(gameAccelerationModel.GameAccelerationMultiplier);
            attackModel.CooldownTimer.Set(cooldown / gameAccelerationModel.GameAccelerationMultiplier);
            _eventBus.Subscribe<GameAccelerationChangedEvent>(speed =>
                OnGameAccelerationChanged(attackModel, gameAccelerationModel, towerAnimatorController,
                    cooldown));

            _towerInstantiated?.Invoke(instance.transform);
        }

        private static TowerLevelConfig GetLevelConfig(TowerConfig towerConfig, int towerLevel,
            ref MissileConfig missileConfig)
        {
            TowerLevelConfig levelConfig = null;

            switch (towerConfig)
            {
                case BomberTowerConfig bomberTowerConfig:
                    levelConfig = bomberTowerConfig.GetCurrentTowerLevelConfig(towerLevel);
                    missileConfig = bomberTowerConfig.MissileConfig;
                    break;
                case NotBomberTowerConfig notBomberTowerConfig:
                    levelConfig = notBomberTowerConfig.GetCurrentTowerLevelConfig(towerLevel);
                    missileConfig = notBomberTowerConfig.MissileConfig;
                    break;
                default:
                    return null;
            }

            return levelConfig;
        }

        public TowerComponents CreateTower(TowerConfig towerConfig, TowerLevelConfig towerLevelConfig, Vector3 position)
        {
            var instance = Object.Instantiate(towerConfig.TowerPrefab, position + towerConfig.Offset,
                Quaternion.identity);

            var color = instance.SpriteRenderer.color;
            instance.SpriteRenderer.color = new Color(color.r, color.g, color.b, color.a / 2);
            
            if (towerLevelConfig.AnimatorOverrideController != null)
                instance.AnimatorView.Animator.runtimeAnimatorController = towerLevelConfig.AnimatorOverrideController;

            if (towerLevelConfig.TowerSprite != null)
                instance.SpriteRenderer.sprite = towerLevelConfig.TowerSprite;
            
            return instance;
        }

        private void OnGameAccelerationChanged(TowerAttackingRealtimeModel attackingModel,
            GameAccelerationModel model, TowerAnimatorController towerAnimatorController,
            float cooldown)
        {
            attackingModel.CooldownTimer.SetCountingTimeWithoutRestart(
                cooldown / model.GameAccelerationMultiplier);
            towerAnimatorController.ChangeAnimationSpeedMultiplier(model.GameAccelerationMultiplier);
        }

        private T InstantiateFromData<T>(T prefab, TransformConfig config) where T : Object
            => Object.Instantiate(prefab, config.Position, Quaternion.Euler(config.Rotation));
    }
}
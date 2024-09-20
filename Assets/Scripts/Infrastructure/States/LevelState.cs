using System;
using System.Collections.Generic;
using Systems.Enemy.EnemyAnimatorController;
using Systems.Level;
using Systems.Level.Configs;
using Systems.Level.GameAcceleration;
using Systems.Level.LevelPopup;
using Systems.Level.LevelWaves;
using Systems.UI.BuildingTowerPopup;
using Cysharp.Threading.Tasks;
using Data;
using Holders;
using Infrastructure.EventBusSystem;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Logic.Observers;
using Logic.Other;
using Logic.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LevelState : IState
    {
        private readonly IStaticDataProviderService _staticDataProviderService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly CurrenciesProviderService _currenciesProviderService;
        private readonly GameDataProviderAndSaverService _gameDataProviderAndSaverService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly TimeScaleChangingService _timeScaleChangingService;
        private readonly IEventBus _eventBus;

        public LevelState(IStaticDataProviderService staticDataProviderService, IGameFactory gameFactory,
            IUIFactory uiFactory, CurrenciesProviderService currenciesProviderService,
            GameDataProviderAndSaverService gameDataProviderAndSaverService, GameStateMachine gameStateMachine,
            TimeScaleChangingService timeScaleChangingService, IEventBus eventBus)
        {
            _staticDataProviderService = staticDataProviderService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _currenciesProviderService = currenciesProviderService;
            _gameDataProviderAndSaverService = gameDataProviderAndSaverService;
            _gameStateMachine = gameStateMachine;
            _timeScaleChangingService = timeScaleChangingService;
            _eventBus = eventBus;
        }

        public async UniTask Enter()
        {
            await SceneManager.LoadSceneAsync(SceneNames.Game);
            _gameFactory.ClearActions();

            _timeScaleChangingService.ChangeToOneScale();
            var gameData = _gameDataProviderAndSaverService.GameData;
            var levelConfig = _staticDataProviderService.GameConfig.LevelsConfig.LevelConfigs[Mathf.Min(gameData.OpenLevelIndex, 
                _staticDataProviderService.GameConfig.LevelsConfig.LevelConfigs.Count - 1)];

            var mapInstance = _gameFactory.CreateMap(levelConfig.Map);
            var enemyWays = mapInstance.Ways;

            var fixer = new CameraRatioFixer(mapInstance);
            InitializeGameAccelerationModelAndController(out var gameAccelerationModel);
            InitializeLifeCounter(out var lifeCounter);
            InitializeLevelWaves(levelConfig, enemyWays, out var levelWavesModel, gameAccelerationModel, lifeCounter);
            InitializeBuildingTower(gameAccelerationModel, _uiFactory.GetLevelPopup(), _gameFactory.CreateTouchObserver(),
                out var buildingTowerModel);
            InitializeLevelPopup(_uiFactory.GetBuildingTowerPopup());

            _gameFactory.CreateOrderSortingByPositionYController();
            
            var audioPlayer = _gameFactory.CreateBackgroundAudioPlayer();

            audioPlayer.SetAudioClipsAndTryPlayNextSound(_staticDataProviderService.GameConfig.SoundsConfig.LevelMusicClips);
        }

        public UniTask Exit()
        {
            _gameFactory.ClearActions();
            _eventBus.UnsubscribeAll<GameAccelerationChangedEvent>();
            return UniTask.CompletedTask;
        }

        private void InitializeLevelWaves(LevelConfig levelConfig, List<Way> enemyWays,
            out LevelWavesModel levelWavesModel, GameAccelerationModel gameAccelerationModel, Counter lifeCounter)
        {
            var levelPopup = _uiFactory.GetLevelPopup();
            levelPopup.OpenPopup();
            levelWavesModel = new LevelWavesModel(levelConfig, levelPopup, enemyWays,
                _gameFactory, _gameStateMachine, _staticDataProviderService.GameConfig, _uiFactory, _gameDataProviderAndSaverService,
                _currenciesProviderService, _eventBus, lifeCounter);
            var waveController = new LevelWavesController(levelWavesModel, levelPopup, gameAccelerationModel);
        }

        private void InitializeGameAccelerationModelAndController(out GameAccelerationModel gameAccelerationModel)
        {
            var levelPopup = _uiFactory.GetLevelPopup();
            
            gameAccelerationModel = new GameAccelerationModel(levelPopup, _currenciesProviderService, 
                _staticDataProviderService.GameConfig, _eventBus);
            var gameAccelerationController = new GameAccelerationController(levelPopup, gameAccelerationModel);
        }

        private void InitializeLevelPopup(BuildingTowerPopupView buildingTowerPopupView)
        {
            var levelPopup = _uiFactory.GetLevelPopup();
            
            var levelPopupModel = new LevelPopupModel(_currenciesProviderService, _gameDataProviderAndSaverService, 
                 _staticDataProviderService.GameConfig, _uiFactory, levelPopup, buildingTowerPopupView);
            var levelPopupController = new LevelPopupController(levelPopup, _uiFactory.GetBuildingTowerPopup(), levelPopupModel);
        }

        private void InitializeLifeCounter(out Counter counter)
        {
            var startCount = _staticDataProviderService.GameConfig.LevelsConfig
                .NumberEnemiesWhoEndedPathBeforeShowDefeatPopup;
            counter = new Counter(startCount);

            _uiFactory.GetLevelPopup().LifeCounterText.text = startCount.ToString();

            var lifeCounter = counter;
            _gameFactory.CertainEnemyEndedPath += (instance) => lifeCounter.Reduce(instance.IsBoss
            ? _staticDataProviderService.GameConfig.LevelsConfig.NumberLifeWhichBossTakesAway 
            : _staticDataProviderService.GameConfig.LevelsConfig.NumberLifeWhichNotBossTakesAway);
            counter.EndedCounting += () => _uiFactory.GetDefeatPopup(_gameStateMachine).OpenPopup();
            counter.EndedCounting += () => _gameDataProviderAndSaverService.SaveData();
            counter.Reduced += count => _uiFactory.GetLevelPopup().LifeCounterText.text = count.ToString();
        }

        private void InitializeBuildingTower(GameAccelerationModel gameAccelerationModel, LevelPopupView levelPopupView,
            TouchObserver2D touchObserver2D, out BuildingTowerModel buildingTowerModel)
        {
            var buildingTowerPopup = _uiFactory.GetBuildingTowerPopup();
            buildingTowerModel = new BuildingTowerModel(_staticDataProviderService.GameConfig, _uiFactory,
                _gameFactory, _gameDataProviderAndSaverService.GameData);
            var buildingTowerController = new BuildingTowerController(buildingTowerModel,
                levelPopupView, buildingTowerPopup, touchObserver2D, gameAccelerationModel);
        }
    }
}
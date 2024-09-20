using Systems.Level.GameAcceleration;
using Systems.Level.LevelPopup;
using Systems.Tower.Configs;
using Systems.Tower.TowerPlace;
using Systems.UI.BuildingTowerPopup.BuildingButton;
using Logic.Observers;
using Tools;
using UnityEngine;

namespace Systems.UI.BuildingTowerPopup
{
    public class BuildingTowerController
    {
        private readonly GameAccelerationModel _gameAccelerationModel;
        private readonly BuildingTowerModel _model;
        private readonly TouchObserver2D _observer;

        public BuildingTowerController(BuildingTowerModel model, LevelPopupView levelPopupView,
            BuildingTowerPopupView buildingTowerPopupView, TouchObserver2D touchObserver2D,
            GameAccelerationModel gameAccelerationModel)
        {
            _model = model;
            _observer = touchObserver2D;
            _gameAccelerationModel = gameAccelerationModel;
            
            levelPopupView.SetActiveStateBuildingPopupButton.onClick.AddListener(buildingTowerPopupView.SetOpenState);
            CreateBuildingTowerButtons(buildingTowerPopupView);
        }

        private void CreateBuildingTowerButtons(BuildingTowerPopupView buildingTowerPopupView)
        {
            foreach (var config in _model.GameConfig.BuildingTowerConfigs)
            {
                var data = _model.GameData.TowerUpgradeData.Find(towerUpgradeData =>
                    towerUpgradeData.TowerIndex == config.TowerConfig.TowerIndex);
                
                if (!data.IsPurchased())
                    continue;
                
                var buttonView = _model.UIFactory.GetBuildingButtonView(config, buildingTowerPopupView.ButtonsParent);
                var model = new BuildingButtonModel(config);
                var controller = new BuildingButtonController(buttonView, model);
                model.BuildingButtonClicked += SubscribeObserverAndChangeTowerConfig;
            }
        }

        private void SubscribeObserverAndChangeTowerConfig(TowerConfig towerConfig, BuildingButtonView view)
        {
            _model.LastClickedButtonView = view;
            _model.CurrentUnbuiltTowerConfig = towerConfig;
            _observer.Touched += OnTouchScreenInRightPlace;
        }

        private void UnsubscribeObserver() => _observer.Touched -= OnTouchScreenInRightPlace;

        private void CreateUnbuiltTower()
        {
            var towerInstance =
                _model.GameFactory.CreateTower(_model.CurrentUnbuiltTowerConfig, 
                    _model.CurrentUnbuiltTowerConfig.GetCurrentTowerLevelConfig(_model.GameData.TowerUpgradeData), GetTowerPosition());

            _model.CurrentUnbuiltTowerInstance = towerInstance;
        }

        private void DestroyUnbuiltTower()
        {
            Object.Destroy(_model.CurrentUnbuiltTowerInstance);
            UnsubscribeObserver();
        }

        private void OnTouchScreenInRightPlace(RaycastHit2D hit)
        {
            var isTowerOverTowerPlace = IsTowerOverTowerPlace(hit);
            var isCanBuildTowerHere = IsCanBuildTowerHere(hit);

            if (isTowerOverTowerPlace && isCanBuildTowerHere)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    BuildTower();
                    return;
                }
                
                if (!_model.CurrentUnbuiltTowerInstance)
                {
                    CreateUnbuiltTower();
                }

                MoveCurrentTower();
                
            }
            else if (_model.CurrentUnbuiltTowerInstance && Input.GetKeyUp(KeyCode.Mouse0))
            {
                DestroyUnbuiltTower();
                UnsubscribeObserver();
                return;
            }

            if (_model.CurrentUnbuiltTowerInstance)
                _model.CurrentUnbuiltTowerInstance.gameObject.SetActive(isTowerOverTowerPlace && isCanBuildTowerHere);
        }

        private void BuildTower()
        {
            _model.LastTowerPlaceView.Model.TowerOnPlace = _model.CurrentUnbuiltTowerInstance;
            _model.LastTowerPlaceView.HidePlate();
            ConstructTower();
            _model.CurrentUnbuiltTowerInstance = null;
            _model.CurrentUnbuiltTowerConfig = null;
            _model.LastTowerPlaceView = null;
            DisableLastClickedButton();
            UnsubscribeObserver();
        }

        private void DisableLastClickedButton()
        {
            _model.LastClickedButtonView.SetActiveGraphicOnDisable(true);
            _model.LastClickedButtonView.PointerInputObserver.enabled = false;
            _model.LastClickedButtonView = null;
        }

        private void ConstructTower()
        {
            _model.GameFactory.ConstructTower(_model.CurrentUnbuiltTowerInstance, _model.CurrentUnbuiltTowerConfig,
                _gameAccelerationModel,
                _model.CurrentUnbuiltTowerConfig.GetLevel(_model.GameData.TowerUpgradeData));
        }

        private void MoveCurrentTower() => _model.CurrentUnbuiltTowerInstance.transform.position = GetTowerPosition();

        private Vector2 GetTowerPosition() =>
            _model.LastTowerPlaceView.transform.position +
            _model.CurrentUnbuiltTowerConfig.Offset;

        private bool IsCanBuildTowerHere(RaycastHit2D hit)
        {
            return hit.collider && (hit.collider.gameObject == _model.LastTowerPlaceView?.gameObject ||
                     hit.collider.TryGetComponent<TowerPlaceView>(out _model.LastTowerPlaceView))
                   && !_model.LastTowerPlaceView.Model.IsBusy;
        }

        private bool IsTowerOverTowerPlace(RaycastHit2D hit)
        {
            return hit.collider && PhysicsTools.IsLayerMaskContainsLayer(_model.GameConfig.TowerPlaceLayerMask,
                hit.collider.gameObject.layer);
        }
    }
}
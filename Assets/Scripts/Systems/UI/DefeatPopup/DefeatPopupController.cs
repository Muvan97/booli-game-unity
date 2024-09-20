using Systems.Level.LevelWaves;
using Systems.UI.BuildingTowerPopup;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine.UI;

namespace Systems.UI.DefeatPopup
{
    public class DefeatPopupController
    {
        public DefeatPopupController(DefeatPopupView popupView, GameStateMachine gameStateMachine,
            TimeScaleChangingService timeScaleChangingService)
        {
            timeScaleChangingService.ChangeToZeroScale();
            Subscribe(popupView, gameStateMachine);
        }

        private void Subscribe(DefeatPopupView popupView, GameStateMachine gameStateMachine)
        {
            popupView.AgainButton.onClick.AddListener(gameStateMachine.Enter<LevelState>);
            
            popupView.HomeButton.onClick.AddListener(gameStateMachine.Enter<MainMenuState>);
        }
    }
}
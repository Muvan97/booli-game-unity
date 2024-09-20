using Systems.Level.LevelPopup;

namespace Systems.Level.GameAcceleration
{
    public class GameAccelerationController
    {
        private readonly LevelPopupView _view;
        private readonly GameAccelerationModel _model;

        public GameAccelerationController(LevelPopupView levelPopupView, GameAccelerationModel model)
        {
            _view = levelPopupView;
            _model = model;
            //_model.GameAccelerationTimer.Set(_model.GameConfig.TimeGameAcceleration);
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            _view.GameAccelerationButton.onClick.AddListener(OnClickGameAccelerationButton);
            _model.GameAccelerationTimer.Ended += OnEndGameAccelerationTimer;
        }
        
        private void OnEndGameAccelerationTimer()
        {
            _view.GameAccelerationButton.enabled = true;
            _model.MakeDefaultTimeScaleMultiplier();
            FireGameAccelerationChangedEvent();
        }
        
        private void OnClickGameAccelerationButton()
        {
            if (!_model.IsCanUsingGameAcceleration)
                return;

            _model.CurrenciesProviderService.Coins.Number -= _model.GameConfig.PriceForUsingGameAcceleration;
            _view.GameAccelerationButton.enabled = false;
            //_model.GameAccelerationTimer.StartCountingTime();
            _model.MakeIncreasedGameAccelerationMultiplier();
            FireGameAccelerationChangedEvent();
        }
        
        private void FireGameAccelerationChangedEvent() => _model.EventBus.Fire(new GameAccelerationChangedEvent(_model.GameAccelerationMultiplier));
    }
}
using Systems.Level.LevelPopup;
using Configs;
using Infrastructure.EventBusSystem;
using Infrastructure.Services;
using Logic.Other;

namespace Systems.Level.GameAcceleration
{
    public class GameAccelerationModel
    {
        public bool IsCanUsingGameAcceleration => CurrenciesProviderService.Coins.Number >= GameConfig.PriceForUsingGameAcceleration;
        public float GameAccelerationMultiplier { get; private set; }
        public readonly CurrenciesProviderService CurrenciesProviderService;
        public readonly GameConfig GameConfig;
        public readonly IEventBus EventBus;
        public Timer GameAccelerationTimer { get; private set; }

        public GameAccelerationModel(LevelPopupView levelPopupView, CurrenciesProviderService currenciesProviderService, 
            GameConfig gameConfig, IEventBus eventBus)
        {
            CurrenciesProviderService = currenciesProviderService;
            GameConfig = gameConfig;
            EventBus = eventBus;
            GameAccelerationTimer = new Timer(levelPopupView.destroyCancellationToken);
            MakeDefaultTimeScaleMultiplier();
        }
        
        public void MakeDefaultTimeScaleMultiplier() => GameAccelerationMultiplier = GameConfig.GameAccelerationMultiplierWhenGameAccelerationInactive; 
        public void MakeIncreasedGameAccelerationMultiplier() => GameAccelerationMultiplier = GameConfig.GameAccelerationMultiplierWhenGameAccelerationActive;
    }
}
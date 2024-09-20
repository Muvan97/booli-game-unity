using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticData;
using Debug = UnityEngine.Debug;

namespace Infrastructure.States
{
    public class LoadingDataState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingDataService _loadingDataDataService;

        public LoadingDataState(GameStateMachine gameStateMachine, LoadingDataService loadingDataDataService)
        {
            _gameStateMachine = gameStateMachine;
            _loadingDataDataService = loadingDataDataService;
        }

        public async UniTask Enter()
        {           
            await _loadingDataDataService.LoadData();

            _gameStateMachine.Enter<MainMenuState>(); }
        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
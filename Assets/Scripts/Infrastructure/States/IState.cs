using Systems.UI.UpgradeTowerPopup;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}
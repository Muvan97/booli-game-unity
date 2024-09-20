using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}
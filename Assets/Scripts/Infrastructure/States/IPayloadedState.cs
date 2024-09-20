using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload sceneName);
    }

    public interface IPayloadedState<TPayload1, TPayload2> : IExitableState
    {
        UniTask Enter(TPayload1 payload1, TPayload2 payload2);
    }
    
    public interface IPayloadedState<TPayload1, TPayload2, TPayload3> : IExitableState
    {
        UniTask Enter(TPayload1 payload1, TPayload2 payload2, TPayload3 payload3);
    }
}
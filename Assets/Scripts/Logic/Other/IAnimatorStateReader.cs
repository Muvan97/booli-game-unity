namespace Logic.Other
{
    public interface IAnimatorStateReader : IExitedAnimatorStateReader
    {
        //AnimatorState State { get; }
        void EnteredState(int stateHash);
    }
}
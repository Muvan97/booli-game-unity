using System;
using Logic.Other;
using UnityEngine;

namespace Logic.Observers
{
    public class AnimatorStateObserver : MonoBehaviour, IAnimatorStateReader
    {
        public Action<int> Entered, Exited;

        public void ExitedState(int stateHash) => Exited?.Invoke(stateHash);

        public void EnteredState(int stateHash) => Entered?.Invoke(stateHash);
    }
}
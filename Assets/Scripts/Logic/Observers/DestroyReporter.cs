using System;
using UnityEngine;

namespace Logic.Observers
{
    public class DestroyReporter : MonoBehaviour
    {
        public Action Destroyed;
        private void OnDestroy() => Destroyed?.Invoke();
    }
}
using System;
using UnityEngine;

namespace Logic.Observers
{
    public class MonoBehaviourObserver : MonoBehaviour
    {
        public Action Updated, FixedUpdated, DrawedGizmos, Enabled, Disabled;

        private void Update() => Updated?.Invoke();

        private void FixedUpdate() => FixedUpdated?.Invoke();

        private void OnDrawGizmos() => DrawedGizmos?.Invoke();

        private void OnEnable() => Enabled?.Invoke();
        public void OnDisable() => Disabled?.Invoke();
    }
}
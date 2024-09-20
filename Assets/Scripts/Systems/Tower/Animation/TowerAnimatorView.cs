using System;
using Logic.Observers;
using UnityEngine;

namespace Systems.Tower.Animation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AnimatorStateObserver))]
    public class TowerAnimatorView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public AnimatorStateObserver AnimatorStateObserver { get; private set; }
        
        public Action AttackAnimationEventInvoked;

        public void InvokeAttackAnimationEventInvokedAction() => AttackAnimationEventInvoked?.Invoke();
    }
}
using Systems.Enemy.EnemyAnimatorController;
using Systems.Enemy.EnemyMovingAndRotation;
using Systems.EntityHealth;
using Infrastructure.States;
using Logic.Observers;
using Logic.Other;
using Logic.UI;
using UnityEngine;

namespace Systems.Enemy
{
    [RequireComponent(typeof(EnemyMovingView))]
    [RequireComponent(typeof(EntityHealthView))]
    [RequireComponent(typeof(MonoBehaviourObserver))]
    [RequireComponent(typeof(EnemyAnimatorView))]
    [RequireComponent(typeof(DestroyReporter))]
    [RequireComponent(typeof(AudioSource))]
    public class EnemyComponents : MonoBehaviour
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public EntityHealthView HealthView { get; private set; }
        [field: SerializeField] public MonoBehaviourObserver Observer { get; private set; }
        [field: SerializeField] public EnemyAnimatorView AnimatorView { get; private set; }
        [field: SerializeField] public HealthSliderView HealthSliderView { get; private set; }
        public EntityHealthController EntityHealthController => HealthView.Controller;
        public bool IsBoss;
    }
}
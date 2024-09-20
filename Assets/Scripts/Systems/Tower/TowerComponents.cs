using Systems.Tower.Animation;
using Systems.Tower.Attacking;
using Logic.Observers;
using UnityEngine;

namespace Systems.Tower
{
    [RequireComponent(typeof(MonoBehaviourObserver))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class TowerComponents : MonoBehaviour
    {
        [field: SerializeField] public TowerAnimatorView AnimatorView { get; private set; }
        [field: SerializeField] public TowerAttackingView AttackingView { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}
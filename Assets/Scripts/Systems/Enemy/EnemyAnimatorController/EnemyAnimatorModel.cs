using UnityEngine;

namespace Systems.Enemy.EnemyAnimatorController
{
    public class EnemyAnimatorModel
    {
        public readonly int AnimationsSpeedMultiplierHash = Animator.StringToHash("AnimationsSpeedMultiplier");
        public readonly int IsDiedHash = Animator.StringToHash("IsDead");
        public readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    }
}
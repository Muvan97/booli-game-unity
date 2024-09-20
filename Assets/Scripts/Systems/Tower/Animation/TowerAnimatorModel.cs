using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Tower.Animation
{
    public class TowerAnimatorModel
    {
        public Action<TowerAnimation> ExitedAnimation;
        public int AttackTriggerHash => Animator.StringToHash("Attack");
        public int StopAttackTriggerHash => Animator.StringToHash("StopAttack");
        public int AnimationsSpeedMultiplierHash => Animator.StringToHash("AnimationsSpeedMultiplier");

        public readonly IReadOnlyDictionary<int, TowerAnimation> TowerAnimationsByHash 
            = new Dictionary<int, TowerAnimation> 
                {{Animator.StringToHash("Attacking"), TowerAnimation.Attacking}};
        
    }
}
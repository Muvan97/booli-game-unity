using UnityEngine;

namespace Systems.Tower.Missile.Attacking
{
    public class BombMissileAttackingModel : MissileAttackingModel
    {
        public readonly float BombRadius;
        public readonly Vector3 ExplosionPosition;
        public readonly LayerMask EnemyLayerMask;
        public int ExplodeTriggerHash => Animator.StringToHash("Explode");

        public BombMissileAttackingModel(float damage, Vector3 explosionPosition, 
            LayerMask enemyLayerMask, float bombRadius) : base(damage)
        {
            ExplosionPosition = explosionPosition;
            EnemyLayerMask = enemyLayerMask;
            BombRadius = bombRadius;
        }
    }
}
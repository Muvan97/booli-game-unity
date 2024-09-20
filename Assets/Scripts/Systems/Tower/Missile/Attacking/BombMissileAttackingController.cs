using Systems.EntityHealth;
using UnityEngine;

namespace Systems.Tower.Missile.Attacking
{
    public class BombMissileAttackingController : IMissileAttackingController
    {
        private readonly BombMissileAttackingModel _model;

        public BombMissileAttackingController(BombMissileAttackingModel missileAttackingModel) => _model = missileAttackingModel;

        public void DoDamage()
        {
            var enemiesInRadius = Physics2D.OverlapCircleAll(_model.ExplosionPosition, _model.BombRadius, _model.EnemyLayerMask);

            foreach (var enemy in enemiesInRadius)
            {
                if (enemy.TryGetComponent<EntityHealthView>(out var goal))
                    goal.Controller.TakeDamage(_model.Damage);
            }
        }
    }
}
using Systems.EntityHealth;

namespace Systems.Tower.Missile.Attacking
{
    public class NotBombMissileAttackingModel : MissileAttackingModel
    {
        public readonly EntityHealthController Goal;
        
        public NotBombMissileAttackingModel(float damage, EntityHealthController goal) 
            : base(damage)
        {
            Goal = goal;
        }
    }
}
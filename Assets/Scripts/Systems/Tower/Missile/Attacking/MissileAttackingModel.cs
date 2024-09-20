namespace Systems.Tower.Missile.Attacking
{
    public abstract class MissileAttackingModel
    {
        public readonly float Damage;

        protected MissileAttackingModel(float damage) => Damage = damage;
    }
}
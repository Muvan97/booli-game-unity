namespace Systems.Tower.Missile.Attacking
{
    public class NotBombAttackingController : IMissileAttackingController
    {
        private readonly NotBombMissileAttackingModel _model;

        public NotBombAttackingController(NotBombMissileAttackingModel missileAttackingModel)
        {
            _model = missileAttackingModel;
        }
        
        public void DoDamage() => _model.Goal.TakeDamage(_model.Damage);
    }
}
using System;

namespace Systems.Tower.Attacking
{
    public abstract class TowerAttackingModel
    {
        public Action Attacked;
        public Action StopAttacked;
    }
}
using UnityEngine;

namespace Systems.EntityHealth
{
    public class EntityHealthController
    {
        public readonly EntityHealthView View;
        public readonly EntityHealthModel Model;

        public EntityHealthController(EntityHealthModel model, EntityHealthView view)
        {
            Model = model;
            View = view;
        }
        
        public void TakeDamage(float damage)
        {
            damage = Mathf.Min(Model.Health, damage);
            Model.Health -= damage;
        }
    }
}
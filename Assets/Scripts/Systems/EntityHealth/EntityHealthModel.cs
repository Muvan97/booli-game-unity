using System;
using UnityEngine;

namespace Systems.EntityHealth
{
    public class EntityHealthModel
    {
        public Action<float> HealthChanged;
        public Action Died;

        public float Health
        {
            get => _health;
            set
            {
                if (value < 0 || IsDied || value == Health)
                    return;

                _health = Mathf.Max(0, value);
                HealthChanged?.Invoke(_health);

                if (_health == 0)
                {
                    IsDied = true;
                    Died?.Invoke();   
                }
            }
        }

        public bool IsDied { get; private set; }
        private float _health;

        public EntityHealthModel(float health) => Health = health;
    }
}
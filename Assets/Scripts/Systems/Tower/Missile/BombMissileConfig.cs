using System;
using UnityEngine;

namespace Systems.Tower.Missile
{
    [Serializable]
    public class BombMissileConfig : MissileConfig
    {
        [field: SerializeField] public float FlyingHeightOnShoot { get; private set; }
        [field: SerializeField] public float TimeAfterExplosionStartBeforeDestroy { get; private set; } = 0.5f;
    }
}
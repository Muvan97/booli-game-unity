using System;
using UnityEngine;

namespace Systems.Tower.Missile
{
    [Serializable]
    public class MissileConfig
    {
        [field: SerializeField] public float FlyingTime { get; private set; }
        [field: SerializeField] public bool IsRotateMissileToGoal { get; private set; }
        [field: SerializeField] public MissileView MissilePrefab { get; private set; }
        [field: SerializeField] public AudioClip Sound { get; private set; }
    }
}
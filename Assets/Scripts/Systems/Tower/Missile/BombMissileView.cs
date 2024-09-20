using UnityEngine;

namespace Systems.Tower.Missile
{
    [RequireComponent(typeof(Animator))]
    public class BombMissileView : MissileView
    {
        [field: SerializeField] public Animator Animator { get; private set; }
    }
}
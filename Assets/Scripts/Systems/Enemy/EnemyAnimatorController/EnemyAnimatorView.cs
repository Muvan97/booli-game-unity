using UnityEngine;

namespace Systems.Enemy.EnemyAnimatorController
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimatorView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
    }
}
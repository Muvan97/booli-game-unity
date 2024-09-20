using UnityEngine;

namespace Systems.Enemy.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float TimeBeforeDestroyAfterDead { get; private set; } = 0.5f;
        [field: SerializeField] public Vector3 PositionOffset { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public EnemyComponents Prefab { get; private set; }
        [field: SerializeField] public long RewardForKill { get; private set; }
        [field: SerializeField] public Currency CurrencyForKill { get; private set; }
    }
}
using UnityEngine;

namespace Systems.Level.Configs
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField] public MapPrefab Prefab { get; private set; }
    }
}
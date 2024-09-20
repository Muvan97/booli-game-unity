using UnityEngine;

namespace Systems.Tower.Missile
{
    [RequireComponent(typeof(AudioSource))]
    public class MissileView : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
    }
}
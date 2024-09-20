using UnityEngine;
using UnityEngine.UI;

namespace Systems.EntityHealth
{
    [RequireComponent(typeof(EntityHealthView))]
    public class HealthSliderView : MonoBehaviour
    {
        [field: SerializeField] public Slider Slider { get; private set; }
    }
}
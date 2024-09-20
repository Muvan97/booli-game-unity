using UnityEngine;

namespace Systems.EntityHealth
{
    public class EntityHealthView : MonoBehaviour
    {
        public EntityHealthController Controller { get; private set; }
        public void Construct(EntityHealthController controller) => Controller = controller;
    }
}
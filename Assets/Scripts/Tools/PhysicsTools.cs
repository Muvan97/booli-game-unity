using UnityEngine;

namespace Tools
{
    public static class PhysicsTools
    {
        public static bool IsLayerMaskContainsLayer(LayerMask layerMask, int layer)
            => (layerMask & (1 << layer)) != 0;

        public static int GetLayerFromLayerMask(LayerMask layerMask)
            => (int) Mathf.Log(layerMask.value, 2);
    }
}
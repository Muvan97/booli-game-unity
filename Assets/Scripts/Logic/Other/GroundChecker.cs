using UnityEngine;

namespace Logic.Other
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private float _checkSphereRadius;
        [SerializeField] private LayerMask _groundLayerMask;
        public bool IsHasGround() => Physics.OverlapSphere(transform.position, _checkSphereRadius, _groundLayerMask).Length > 0;
    }
}
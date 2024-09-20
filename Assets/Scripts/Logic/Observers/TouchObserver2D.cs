using System;
using UnityEngine;

namespace Logic.Observers
{
    public class TouchObserver2D : MonoBehaviour
    {
        public Action<RaycastHit2D> Touched;

        private Camera _camera;

        private void Start() => _camera = Camera.main;

        private void Update()
        {
            if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyUp(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0))
                return;

            var hit = GetHit();
            
            Touched?.Invoke(hit);
        }

        private RaycastHit2D GetHit()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            return hit;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Logic.Observers
{
    public class OverlapCircleObserver : MonoBehaviour
    {
        public Transform NearestTransform => CustomMathTools.GetNearestTransform(
            _collidersInRadius.Keys.ToList().Select(key => key.transform), transform.position);
        
        public IReadOnlyList<Collider2D> CollidersInRadius => _collidersInRadius.Keys.ToList();
        public Action<Collider2D> ColliderEntered;
        public Action<Collider2D> ColliderExited;
        public Action<Collider2D> CurrentFirstColliderEntered;
        public Action AllCollidersExited;

        [SerializeField] private bool _isDraw;
        [SerializeField] private Color _sphereColor;
        [SerializeField, Min(0)] private float _radius;
        [SerializeField] private LayerMask _targetLayerMask;
        
        private readonly Dictionary<Collider2D, DestroyReporter> _collidersInRadius = 
            new Dictionary<Collider2D, DestroyReporter>();
        private readonly Collider2D[] _enteredColliders = new Collider2D[500];


        public void Construct(LayerMask targetLayerMask, float radius)
        {
            _targetLayerMask = targetLayerMask;
            _radius = radius;
        }
        
        protected void OnEnable() =>
            FixedUpdate();

        private void OnDrawGizmos()
        {
            if (!_isDraw) return;

            Gizmos.color = _sphereColor;
            Gizmos.DrawSphere(transform.position, _radius);
        }

        private void FixedUpdate()
        {
            if (IsFindColliders())
                OnFindColliders();
            
            else if (_collidersInRadius.Count > 0)
                OnDoesntFindColliders();
        }

        private void OnDoesntFindColliders()
        {
            var colliders = _collidersInRadius.Keys.ToList();
            for (var i = colliders.Count - 1; i >= 0; i--)
                RemoveColliderFromList(colliders[i]);
            
            _collidersInRadius.Clear();
        }

        private void RemoveExitedColliders()
        {
            for (var i = 0; i < _collidersInRadius.Count; i++)
            {
                var colliderInRadius = _collidersInRadius.Keys.ToList()[i];

                if (colliderInRadius && colliderInRadius.gameObject != gameObject && !_enteredColliders.Contains(colliderInRadius))
                    RemoveColliderFromList(colliderInRadius);   
                
            }
        }

        private void AddEnteredColliders()
        {
            foreach (var enteredCollider in _enteredColliders)
            {
                if (!enteredCollider || enteredCollider.gameObject == gameObject ||
                    _collidersInRadius.ContainsKey(enteredCollider)) continue;
                
                var destroyReporter = enteredCollider.GetComponent<DestroyReporter>();
                
                if (!destroyReporter)
                    return;
                
                destroyReporter.Destroyed += () => RemoveColliderFromList(enteredCollider);
                
                _collidersInRadius.Add(enteredCollider, destroyReporter);
                ColliderEntered?.Invoke(enteredCollider);
                
                if (_collidersInRadius.Count == 1)
                    CurrentFirstColliderEntered?.Invoke(enteredCollider);
            }
        }

        private void RemoveColliderFromList(Collider2D colliderInRadius)
        {
            if (!this || !colliderInRadius || colliderInRadius.gameObject == gameObject || !_collidersInRadius.ContainsKey(colliderInRadius))
                return;

            ColliderExited?.Invoke(colliderInRadius);
            _collidersInRadius[colliderInRadius].Destroyed -= () => RemoveColliderFromList(colliderInRadius);
            _collidersInRadius.Remove(colliderInRadius);
            
            if (_collidersInRadius.Count == 0)
                AllCollidersExited?.Invoke();
        }

        private void OnFindColliders()
        {
            RemoveExitedColliders();

            AddEnteredColliders();
        }

        private bool IsFindColliders()
        {
            Array.Clear(_enteredColliders, 0, _enteredColliders.Length);
            return Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _enteredColliders, _targetLayerMask) > 0;
        }

        public void SetRadius(int newRadius) =>
            _radius = newRadius;
    }
}
using System;
using UnityEngine;

namespace Logic.Observers
{
    public class ColliderObserver : MonoBehaviour
    {
        public Action<Collider2D> Entered;
        public Action<Collider2D> Exited;

        private void OnTriggerEnter2D(Collider2D other) => Entered?.Invoke(other);

        private void OnTriggerExit2D(Collider2D other) => Exited?.Invoke(other);

        private void OnCollisionEnter2D(Collision2D other) => Entered?.Invoke(other.collider);

        private void OnCollisionExit2D(Collision2D other) => Exited?.Invoke(other.collider);
    }
}
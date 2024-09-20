using Systems.EntityHealth;
using Logic.Observers;
using UnityEngine;

namespace Systems.Enemy.EnemyMovingAndRotation
{
    public class EnemyMovingController
    {
        private readonly EnemyMovingAndRotationModel _andRotationModel;
        private readonly MonoBehaviourObserver _observer;

        public EnemyMovingController(EnemyMovingAndRotationModel andRotationModel, 
            MonoBehaviourObserver observer, EntityHealthModel entityHealthModel)
        {
            _andRotationModel = andRotationModel;
            _observer = observer;
            
            entityHealthModel.Died += () => _andRotationModel.Unsubscribed.Invoke();
            Subscribe();
            Unsubscribe();
        }

        private void Unsubscribe() => _andRotationModel.Unsubscribed += () => _observer.FixedUpdated -= TryUpdatePosition;
        private void Subscribe() => _observer.FixedUpdated += TryUpdatePosition;

        private void TryUpdatePosition()
        {
            var targetPosition = _andRotationModel.GetEnemyPosition(out var isStopped);

            if (isStopped)
                return;

            _observer.transform.position =
                new Vector3(targetPosition.x, targetPosition.y, _observer.transform.position.z);
        }
    }
}
using Logic.Observers;
using UnityEngine;

namespace Systems.Enemy.EnemyMovingAndRotation
{
    public class EnemyRotationController
    {
        private readonly EnemyMovingAndRotationModel _enemyMovingAndRotationModel;
        private readonly Transform _view;

        public EnemyRotationController(EnemyMovingAndRotationModel andRotationModel, Transform view,
            MonoBehaviourObserver observer)
        {
            _enemyMovingAndRotationModel = andRotationModel;
            _view = view;
            observer.FixedUpdated += UpdateRotation;
        }

        private void UpdateRotation() => _view.transform.right = new Vector2(_enemyMovingAndRotationModel.GetEnemyRotation().x, 0);
    }
}
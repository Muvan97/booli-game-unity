using Systems.Enemy.EnemyMovingAndRotation;
using Systems.EntityHealth;
using Logic.Other;

namespace Systems.Enemy.EnemyAnimatorController
{
    public class EnemyAnimatorController
    {
        private readonly EnemyAnimatorModel _model;
        private readonly EnemyAnimatorView _view;

        public EnemyAnimatorController(EnemyAnimatorModel model, EnemyAnimatorView view,
            EntityHealthModel entityHealthModel, EnemyMovingAndRotationModel enemyMovingAndRotationModel)
        {
            _model = model;
            _view = view;
            entityHealthModel.Died += () => _view.Animator.SetTrigger(_model.IsDiedHash);
            enemyMovingAndRotationModel.ChangedMovingState += ChangeMovingBool;
        }

        public void ChangeAnimationSpeedMultiplier(float value)
        {
            if (_view.Animator)
                _view.Animator.SetFloat(_model.AnimationsSpeedMultiplierHash, value);
        }

        private void ChangeMovingBool(bool isMoving) => _view.Animator.SetBool(_model.IsMovingHash, isMoving);
    }
}
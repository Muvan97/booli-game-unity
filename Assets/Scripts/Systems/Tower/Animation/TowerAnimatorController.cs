

using Systems.Tower.Attacking;

namespace Systems.Tower.Animation
{
    public class TowerAnimatorController
    {
        private readonly TowerAnimatorView _view;
        private readonly TowerAnimatorModel _model;

        public TowerAnimatorController(TowerAnimatorView view, TowerAnimatorModel model, 
            TowerAttackingModel attackModel)
        {
            _view = view;
            _model = model;

            Subscribe(attackModel);
        }

        private void Subscribe(TowerAttackingModel attackModel)
        {
            _view.AnimatorStateObserver.Exited += InvokeExitedAnimation;
            attackModel.Attacked += SetTriggerAttack;
            attackModel.StopAttacked += SetTriggerStopAttack;
        }
        
        public void ChangeAnimationSpeedMultiplier(float value) => _view.Animator.SetFloat(_model.AnimationsSpeedMultiplierHash, value);  

        private void InvokeExitedAnimation(int hash) => _model.ExitedAnimation.Invoke(_model.TowerAnimationsByHash[hash]);

        private void SetTriggerAttack() => _view.Animator.SetTrigger(_model.AttackTriggerHash);
        private void SetTriggerStopAttack() => _view.Animator.SetTrigger(_model.StopAttackTriggerHash);
    }
}
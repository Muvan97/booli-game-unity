using Systems.Tower.Animation;

namespace Systems.Tower.Attacking
{
    public class TowerAttackingController
    {
        private readonly TowerAttackingRealtimeModel _model;
        private readonly TowerAttackingView _view;

        public TowerAttackingController(TowerAttackingRealtimeModel model,
            TowerAttackingView view, TowerAnimatorView towerAnimatorView, TowerAnimatorModel towerAnimatorModel)
        {
            _model = model;
            _view = view;

            Subscribe(towerAnimatorView, towerAnimatorModel);
        }

        private void Subscribe(TowerAnimatorView towerAnimatorView, TowerAnimatorModel towerAnimatorModel)
        {
            if (_model.TowerConfig.IsHasAttackAnimation)
            {
                _model.CircleObserver.ColliderEntered += (collider) => InvokeAttackedIfCan();
                towerAnimatorView.AttackAnimationEventInvoked += InstantiateMissileAndDoDamageAfterFlyingTimeIfCan;
                towerAnimatorModel.ExitedAnimation += MakeModelIsEndPastAttackedAnimationTrueIfCan;
                _model.CooldownTimer.Ended += InvokeAttackedIfCan;
            }
            else
            {
                _model.CircleObserver.CurrentFirstColliderEntered +=
                    col => InstantiateMissileAndDoDamageAfterFlyingTimeIfCan();
                _model.CooldownTimer.Ended += InstantiateMissileAndDoDamageAfterFlyingTimeIfCan;
            }
        }

        private void RestartCooldownTimerOrInstantiateMissile()
        {
            if (_model.IsCanAttackWithoutCooldown)
                InstantiateMissileAndDoDamageAfterFlyingTimeIfCan();
            else
                _model.CooldownTimer.StartCountingTime();
        }

        private void MakeModelIsEndPastAttackedAnimationTrueIfCan(TowerAnimation animation)
        {
            if (_model.IsEndAttackingAnimation(animation))
                _model.IsEndPastAttackAnimation = true;
        }
        
        private void InvokeAttackedIfCan()
        {
            if (!_model.IsCanInvokeAttacked())
                return;

            _model.Attacked?.Invoke();
            _model.IsEndPastAttackAnimation = false;
        }

        private void InstantiateMissileAndDoDamageAfterFlyingTimeIfCan()
        {
            if (_model.CooldownTimer.IsCounting())
                return;

            var goal = _model.Goal;

            if (goal == null)
            {
                if (_model.TowerConfig.IsHasAttackAnimation)
                    _model.StopAttacked?.Invoke();

                return;
            }

            RestartCooldownTimerOrInstantiateMissile();

            _model.GameFactory.InstantiateMissileAndStartAnimation(
                _model.MissileConfig, _view.MissileOrigin.transform.position,
                goal, _model.GameAccelerationModel, out var missileAttackingController, out var animation,
                _model.TowerConfig, _model.Level);

            animation.AnimationEnded += missileAttackingController.DoDamage;
        }
    }
}
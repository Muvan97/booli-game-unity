using Logic.Other;
using UnityEngine;

namespace Logic.Observers
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {

        private IExitedAnimatorStateReader _stateReader;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            TryFindReader(animator);

            if (_stateReader is IAnimatorStateReader stateReader)
            {
                stateReader.EnteredState(stateInfo.shortNameHash);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            TryFindReader(animator);
            
            _stateReader?.ExitedState(stateInfo.shortNameHash);
        }

        private void TryFindReader(Animator animator)
        {
            if (_stateReader != null)
                return;
            
            _stateReader = animator.gameObject.GetComponent<IExitedAnimatorStateReader>();
        }
    }
}
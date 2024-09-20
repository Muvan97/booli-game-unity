using UnityEngine;

namespace Logic.Other
{
    public class RotatorToGoal : MonoBehaviour
    {
        private Transform _goal;
        
        public void Construct(Transform goal) => _goal = goal;

        public void RotateToGoal()
        {
            if (!_goal)
                return;
            
            transform.forward = _goal.position - transform.position;
        }
    }
}
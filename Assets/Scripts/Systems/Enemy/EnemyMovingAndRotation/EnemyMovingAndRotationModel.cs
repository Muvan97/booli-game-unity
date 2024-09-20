using System;
using System.Collections.Generic;
using Systems.Enemy.Configs;
using Systems.Level.Configs;
using Systems.Level.GameAcceleration;
using Tools;
using UnityEngine;

namespace Systems.Enemy.EnemyMovingAndRotation
{
    public class EnemyMovingAndRotationModel
    {
        public Action<bool> ChangedMovingState;
        public bool IsMoving { get; private set; } = true;
        public Action Unsubscribed;
        public Action EndedPath;

        private int _currentWayIndex;
        private float _currentT;
        private readonly List<Way> _ways;
        private readonly EnemyConfig _enemyConfig;
        
        private readonly EnemyMovingView _view;
        private readonly GameAccelerationModel _gameAccelerationModel;

        public EnemyMovingAndRotationModel(EnemyConfig enemyConfig, GameAccelerationModel gameAccelerationModel, List<Way> ways)
        {
            _enemyConfig = enemyConfig;
            _gameAccelerationModel = gameAccelerationModel;
            _ways = ways;
        }

        public void UpdateCurrentT()
        {
            _currentT += 1 / (_ways[_currentWayIndex].Length /
                              (_enemyConfig.Speed * _gameAccelerationModel.GameAccelerationMultiplier) *
                              (1 / Time.fixedDeltaTime));
        }

        public Vector3 GetEnemyRotation() => BezierTools.GetFirstDerivative(_ways[_currentWayIndex].Points, _currentT);

        public Vector3 GetEnemyPosition(out bool isStopped)
        {
            isStopped = false;
            
            if (IsReachedNextPathPoint())
            {
                isStopped = _currentWayIndex + 1 >= _ways.Count;
         
                if (isStopped)
                {
                    IsMoving = false;
                    ChangedMovingState?.Invoke(IsMoving);
                    Unsubscribed?.Invoke();
                    EndedPath?.Invoke();
                    return Vector3.zero;   
                }
                
                _currentWayIndex++;
                _currentT %= 1;
            }

            return BezierTools.GetPoint(_ways[_currentWayIndex].Points, _currentT) + _enemyConfig.PositionOffset;
        }

        private bool IsReachedNextPathPoint() => _currentT >= 1;
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic.Other;
using Tools;
using UnityEngine;

namespace Systems.Tower.Missile.Animation
{
    public class BombAnimation : IMissileFlyingAnimation
    {
        public Action AnimationEnded { get; set; }

        public readonly Timer Timer;
        private readonly BombMissileConfig _config;
        private readonly Vector2 _endPoint;
        private readonly Vector2 _startPoint;
        private readonly Transform _missile;

        public BombAnimation(BombMissileConfig config,
            Vector3 endPoint, Vector3 startPoint, Transform missile, float gameAccelerationMultiplier)
        {
            _config = config;
            _endPoint = endPoint;
            _startPoint = startPoint;
            _missile = missile;
            Timer = new Timer(missile.gameObject.GetCancellationTokenOnDestroy());

            var points = GetPathPoints(config);
            Timer.Updated += (time) => UpdateAnimation(time, points);
            Timer.Ended += () => AnimationEnded?.Invoke();
            StartAnimation(gameAccelerationMultiplier);
        }

        private List<Vector3> GetPathPoints(BombMissileConfig config)
            => new List<Vector3> {_startPoint, new Vector2(_startPoint.x + _endPoint.x, 
                Mathf.Max(_startPoint.y, _endPoint.y) + config.FlyingHeightOnShoot), _endPoint};

        public void StartAnimation(float gameAccelerationMultiplier) => Timer.StartCountingTime(_config.FlyingTime / gameAccelerationMultiplier);

        private void UpdateAnimation(float time, List<Vector3> points)
        {
            var t = (Timer.AppointedTime - time) / Timer.AppointedTime;
            var position = BezierTools.GetPoint(points, t);
            var missilePositionZ = _missile.transform.position.z;
            _missile.transform.position = new Vector3(position.x, position.y, missilePositionZ);
        }
    }
}
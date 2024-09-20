using System;
using DG.Tweening;
using UnityEngine;

namespace Systems.Tower.Missile.Animation
{
    public class MissileFlyingAnimation : IMissileFlyingAnimation
    {
        public Action AnimationEnded { get; set; }

        private readonly MissileConfig _config;
        private readonly Vector3 _endPoint;
        private readonly Transform _missile;


        public MissileFlyingAnimation(MissileConfig config,
            Vector3 endPoint, Transform missile)
        {
            _config = config;
            _endPoint = endPoint;
            _missile = missile;

            if (_config.IsRotateMissileToGoal)
                RotateMissileToGoal();
        }

        private void RotateMissileToGoal() => _missile.transform.up = _endPoint - _missile.transform.position;

        public void StartAnimation(float gameAccelerationMultiplier)
        {
            var animation = _missile.DOMove(_endPoint, _config.FlyingTime / gameAccelerationMultiplier);
            animation.OnComplete(() => AnimationEnded?.Invoke());
        }
    }
}
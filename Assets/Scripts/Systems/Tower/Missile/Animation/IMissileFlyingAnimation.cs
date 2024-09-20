using System;

namespace Systems.Tower.Missile.Animation
{
    public interface IMissileFlyingAnimation
    {
        public Action AnimationEnded { get; set; }
        void StartAnimation(float gameAccelerationMultiplier);
    }
}
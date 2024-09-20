using Infrastructure.EventBusSystem;

namespace Systems.Level.GameAcceleration
{
    public class GameAccelerationChangedEvent : IEvent
    {
        public readonly float AccelerationMultiplier;
        
        public GameAccelerationChangedEvent(float timeScale) => AccelerationMultiplier = timeScale;
    }
}
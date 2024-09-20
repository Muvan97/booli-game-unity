using UnityEngine;

namespace Infrastructure.Services
{
    public class TimeScaleChangingService : IService
    {
        public void ChangeScale(float scale) => Time.timeScale = scale;
        public void ChangeToZeroScale() => Time.timeScale = 0;
        public void ChangeToOneScale() => Time.timeScale = 1;
    }
}
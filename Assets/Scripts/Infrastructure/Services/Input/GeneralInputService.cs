using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class GeneralInputService : InputService
    {
        public override Vector2 Axis => GetUnityAxis();
        

        public GeneralInputService()
        {
        }
    }
}
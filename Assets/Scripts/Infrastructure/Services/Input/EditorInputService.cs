using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class EditorInputService : InputService
    {
        public EditorInputService()
        {
            
        }

        public override Vector2 Axis => GetUnityAxis();
    }
}
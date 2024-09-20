using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public struct TransformConfig
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }

        public TransformConfig(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
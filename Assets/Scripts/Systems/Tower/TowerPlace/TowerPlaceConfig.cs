using System;
using UnityEngine;

namespace Systems.Tower.TowerPlace
{
    [Serializable]
    public class TowerPlaceConfig
    {
        [field: SerializeField] public TowerPlaceView Point { get; private set; } 
    }
}
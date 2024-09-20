using System;
using Systems.Tower.Configs;
using UnityEngine;

namespace Systems.UI.BuildingTowerPopup.BuildingButton
{
    [Serializable]
    public class BuildingButtonConfig
    {
        [field: SerializeField] public TowerConfig TowerConfig {get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; } 
    }
}
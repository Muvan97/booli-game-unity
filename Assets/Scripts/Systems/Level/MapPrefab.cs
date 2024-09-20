using System.Collections.Generic;
using Systems.Level.Configs;
using Systems.Tower.TowerPlace;
using Logic.Observers;
using UnityEngine;

namespace Systems.Level
{
    public class MapPrefab : MonoBehaviour
    {
        [field: SerializeField] public List<Way> Ways { get; private set; }
        [field: SerializeField] public List<TowerPlaceConfig> TowersPlacesPoints { get; private set; }
    }
}   

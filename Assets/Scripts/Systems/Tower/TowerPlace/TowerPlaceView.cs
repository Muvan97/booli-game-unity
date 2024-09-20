using UnityEngine;

namespace Systems.Tower.TowerPlace
{
    public class TowerPlaceView : MonoBehaviour
    {
        public TowerPlaceModel Model { get; private set; }
        [SerializeField] private GameObject _plate;
        
        public void Construct(TowerPlaceModel model) => Model = model;
        public void HidePlate() => _plate.gameObject.SetActive(false);
    }
}
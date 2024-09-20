namespace Systems.Tower.TowerPlace
{
    public class TowerPlaceModel
    {
        public bool IsBusy => TowerOnPlace != null;
        public TowerComponents TowerOnPlace;
    }
}
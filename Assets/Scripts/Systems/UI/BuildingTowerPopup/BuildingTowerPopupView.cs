using UnityEngine;

namespace Systems.UI.BuildingTowerPopup
{
    public class BuildingTowerPopupView : BasePopup
    {
        [field: SerializeField] public RectTransform BoardTransform { get; private set; }
        [field: SerializeField] public RectTransform ButtonsParent { get; private set; }
    }
}
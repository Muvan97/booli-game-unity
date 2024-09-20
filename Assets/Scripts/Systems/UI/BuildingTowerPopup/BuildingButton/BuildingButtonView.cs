using System.Collections.Generic;
using Logic.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.BuildingTowerPopup.BuildingButton
{
    public class BuildingButtonView : MonoBehaviour
    {
        [field: SerializeField] public PointerInputObserver PointerInputObserver { get; private set; }
        [field: SerializeField] public List<Button> Buttons { get; private set; }
        [SerializeField] private Image _towerImage;
        [SerializeField] private List<Graphic> _graphicOnDisable;

        public void Construct(BuildingButtonConfig data)
        {
            SetActiveGraphicOnDisable(false);
            _towerImage.sprite = data.Sprite;
        }

        public void SetActiveGraphicOnDisable(bool isActive) =>
            _graphicOnDisable.ForEach(graphic => graphic.gameObject.SetActive(isActive));
    }
}
using Logic.Observers;
using UnityEngine.EventSystems;

namespace Systems.UI.BuildingTowerPopup.BuildingButton
{
    public class BuildingButtonController
    {
        private readonly BuildingButtonModel _model;
        private readonly BuildingButtonView _view;

        public BuildingButtonController(BuildingButtonView popup, BuildingButtonModel model)
        {
            _model = model;
            _view = popup;
            
            popup.PointerInputObserver.PointerDowned += InvokeTowerCreatedIfCan;

            _view.PointerInputObserver.PointerUpedWithData += eventData =>
                _view.Buttons.ForEach(button => button.OnPointerUp(eventData));
            
            _view.PointerInputObserver.PointerDownedWithData += eventData =>
                _view.Buttons.ForEach(button => button.OnPointerDown(eventData));
        }
        
        private void InvokeTowerCreatedIfCan() => _model.BuildingButtonClicked?.Invoke(_model.Config.TowerConfig, _view);
    }
}
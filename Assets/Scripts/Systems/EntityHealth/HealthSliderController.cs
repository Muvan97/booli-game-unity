namespace Systems.EntityHealth
{
    public class HealthSliderController
    {
        private readonly HealthSliderView _view;
        public HealthSliderController(EntityHealthModel healthModel, HealthSliderView view)
        {
            _view = view;
            view.Slider.maxValue = healthModel.Health;
            UpdateSliderValue(healthModel.Health);
            healthModel.HealthChanged += UpdateSliderValue;
        }
        
        private void UpdateSliderValue(float value) => _view.Slider.value = value;
    }
}
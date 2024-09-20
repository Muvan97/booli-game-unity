using Systems.UI.UpgradeTowerPopup.UpgradeTowerButton;
using Cysharp.Threading.Tasks;
using Holders;
using Tools;

namespace Data
{
    public class UpgradeTowerButtonController
    {
        private readonly UpgradeTowerButtonModel _model;
        private readonly UpgradeTowerButtonView _view;

        public UpgradeTowerButtonController(UpgradeTowerButtonModel model, UpgradeTowerButtonView view)
        {
            _model = model;
            _view = view;

            if (_model.IsHasMaxLevel())
                OnHasMaxLevel();
            
            UpdateTextsAndTowerImage();
            UpdateTowerNameText().Forget();

            Subscribe(view);

            _view.UpgradeButton.onClick.AddListener(TryUpgradeTower);
        }

        private void Subscribe(UpgradeTowerButtonView view)
        {
            SubscribeTools.SubscribeToLocalizationChanged((locale) =>
            {
                UpdateTowerNameText().Forget();
                UpdateUpgradeText().Forget();
                UpdateCooldownText().Forget();
            }, 
                view.DestroyReporter);
        }

        private async UniTask UpdateUpgradeText()
        {
            _view.UpgradeText.text = await LocalizationTools.GetLocalizedString(
                _model.UpgradeTowerData.IsPurchased() 
                    ? LocalizationKeysHolder.Upgrade 
                    : LocalizationKeysHolder.Buy,
                LocalizationTable.UpgradeTowerPopup);
        }

        private async UniTask UpdateTowerNameText() => _view.TowerNameText.text = await _model.TowerConfig.LocalizedName.GetLocalizedStringAsync();

        private void TryUpgradeTower()
        {
            if (!_model.IsCanUpgradeTower(out var towerUpgradePrice))
                return;

            _model.TowerUpgraded?.Invoke();
            _model.UpgradeTowerData.IncreaseLevel();
            _model.CurrenciesProviderService.Coins.Number -= towerUpgradePrice;
            UpdateTextsAndTowerImage();

            if (_model.IsHasMaxLevel())
                OnHasMaxLevel();

            _model.GameDataProviderAndSaverService.SaveData();
        }

        private void UpdateTextsAndTowerImage()
        {
            UpdateDamageNumberText();
            UpdateLevelNumberText();
            UpdateCooldownText().Forget();
            UpdateUpgradeText().Forget();
            UpdatePriceText();
            UpdateTowerImage();
        }

        private void UpdatePriceText() => _view.PriceText.text = _model.GetTowerUpgradePrice().ToKMBString() + "<sprite=0>";

        private void UpdateTowerImage()
        {
            _view.TowerImage.sprite = _model.GetCurrentTowerLevelConfig().TowerSprite;
            _view.TowerImage.rectTransform.sizeDelta = _model.TowerConfig.TowerImageSizeInUpgradeTowerButtonView;
            _view.TowerImage.transform.localPosition = _model.TowerConfig.TowerImageOffsetInUpgradeTowerButtonView;
        }

        private void UpdateLevelNumberText() => _view.LevelNumberText.text = _model.UpgradeTowerData.Level.ToString();

        private void OnHasMaxLevel()
        {
            _view.PriceText.text = "";
            _view.UpgradeButton.gameObject.SetActive(false);
        }

        private void UpdateDamageNumberText()
        {
            _view.DamageNumberText.text = _model.GetCurrentTowerLevelDamage().ToString();
            
            // if (!_model.IsHasMaxLevel())
            // {
            //     _view.DamageNumberText.text +=
            //         " -> " + _model.GetNextTowerLevelDamage();
            // }
        }

        private async UniTask UpdateCooldownText()
        {
            var shortSecond = await LocalizationTools.GetLocalizedString(
                LocalizationKeysHolder.ShortSecond,
                LocalizationTable.UpgradeTowerPopup);

            _view.CooldownNumberText.text = _model.GetCurrentTowerLevelCooldown() + shortSecond;

            // if (!_model.IsHasMaxLevel())
            // {
            //     _view.CooldownNumberText.text +=
            //         " -> " + _model.GetNextTowerLevelCooldown() + shortSecond;
            // }
        }
    }
}
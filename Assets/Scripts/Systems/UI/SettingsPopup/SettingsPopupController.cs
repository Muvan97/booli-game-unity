using Systems.UI.SettingsPopup.AudioMixerVolume;
using Systems.UI.SettingsPopup.ChangingLanguage;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.StaticData;
using Tools;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    public class SettingsPopupController
    {
        public SettingsPopupController(SettingsPopupView view, Button openButton,
            IStaticDataProviderService staticDataProviderService, GameDataProviderAndSaverService gameDataProviderAndSaverService)
        {
            openButton.onClick.AddListener(view.OpenPopup);
            
            CreateChangingLanguageControllerAndModel(view);
            CreateAudioMixerControllersAndModels(view, staticDataProviderService);

            FillView(view, staticDataProviderService, gameDataProviderAndSaverService).Forget();
        }

        private async UniTask FillView(SettingsPopupView view,
            IStaticDataProviderService staticDataProviderService, GameDataProviderAndSaverService gameDataProviderAndSaverService)
        {
            UITools.InitializeUserNicknameText(view.UserShortNicknameText, gameDataProviderAndSaverService.PlayerNickname, 
                staticDataProviderService.GameConfig.ShortNicknameLength);
            view.UserNicknameText.text = gameDataProviderAndSaverService.PlayerNickname;
            await UITools.UpdateShortNicknameTextAndAvatarRawImage(view.UserAvatarRawImage, 
                gameDataProviderAndSaverService, view.UserShortNicknameText, view.destroyCancellationToken);
        }
        
        private void CreateChangingLanguageControllerAndModel(SettingsPopupView view)
        {
            var model = new ChangingLanguageModel();
            var changeLanguageButtonsController =
                new ChangingLanguageController(view.ChangingLanguageView, model);
        }


        private void CreateAudioMixerControllersAndModels(SettingsPopupView settingsPopupView,
            IStaticDataProviderService staticDataProviderService)
        {
            foreach (var view in settingsPopupView.AudioMixerVolumeViews)
            {
                var audioMixer = staticDataProviderService.GameConfig.AudioMixerConfigs.Find(config =>
                    view.MixerType == config.AudioMixerType).AudioMixer;

                if (audioMixer == null)
                    continue;

                var model = new AudioMixerVolumeModel(audioMixer, view.MixerType);
                var controller = new AudioMixerVolumeController(model, view);
            }
        }
    }
}
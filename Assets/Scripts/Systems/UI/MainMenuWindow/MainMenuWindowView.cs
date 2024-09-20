using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.MainMenuWindow
{
    public class MainMenuWindowView : MonoBehaviour
    {
        [field: SerializeField] public Button OpenTowerUpgradePopupButton { get; private set; }
        [field: SerializeField] public Button OpenDesktopPopupButton { get; private set; }
        [field: SerializeField] public Button OpenShopPopupButton { get; private set; }
    }
}
using Logic.Observers;
using UnitonConnect.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup
{
    [RequireComponent(typeof(DestroyReporter))]
    public class TonWalletConnectionPopup : MonoBehaviour
    {
        [field: SerializeField] public DestroyReporter DestroyReporter { get; private set; }
    }
}
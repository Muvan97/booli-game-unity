using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.SettingsPopup.ChangingLanguage
{
    public class ChangingLanguageView : MonoBehaviour
    {
        [field: SerializeField] public List<Button> Buttons { get; private set; }
    }
}
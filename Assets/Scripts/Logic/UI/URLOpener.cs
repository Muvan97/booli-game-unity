using UnityEngine;

namespace Logic.UI
{
    public class URLOpener : MonoBehaviour
    {
        public void OpenURL(string url) => Application.OpenURL(url);
    }
}
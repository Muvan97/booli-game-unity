using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    #if UNITY_EDITOR
    public static class GameRunner
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ToInitializeScene()
        {
            var scene = SceneManager.GetActiveScene();

            // TODO: delete if when building system is done
            if (scene.buildIndex == 12321)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    #endif
}
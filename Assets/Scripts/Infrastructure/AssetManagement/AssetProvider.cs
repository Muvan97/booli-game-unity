using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public static class AssetProvider
    {
        public static T Instantiate<T>(string path) where T : Object => 
            Instantiate<T>(path, Vector3.zero);

        public static T Instantiate<T>(string path, Transform parent) where T : Object =>
            Instantiate<T>(path, Vector3.zero, Quaternion.identity, parent);

        public static T Instantiate<T>(string path, Transform parent, Vector3 position) where T : Object =>
            Instantiate<T>(path, position, Quaternion.identity, parent);

        public static T Instantiate<T>(string path, Transform parent, Quaternion rotation) where T : Object =>
            Instantiate<T>(path, Vector3.zero, Quaternion.identity, parent);

        public static T Instantiate<T>(string path, Vector3 position) where T : Object => 
            Instantiate<T>(path, position, Quaternion.identity, null);

        public static T Instantiate<T>(string path, Vector3 position, Quaternion rotation) where T : Object => 
            Instantiate<T>(path, position, rotation, null);

        public static T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public static T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);
    }
}
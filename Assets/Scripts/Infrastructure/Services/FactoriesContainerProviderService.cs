using Infrastructure.AssetManagement;
using Logic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class FactoriesContainerProviderService : IService
    {
        public readonly Container Container = new Container();

        public T GetOrLoadAndRegisterMonoBehaviour<T>(string path, bool isInstantiate = true, Transform parent = null)
            where T : MonoBehaviour => GetOrLoadAndRegisterMonoBehaviour<T>(path, out var isLoaded, isInstantiate, parent);

        public T GetOrLoadAndRegisterMonoBehaviour<T>(string path, out bool isLoaded, bool isInstantiate = true,
            Transform parent = null)
            where T : MonoBehaviour
        {
            isLoaded = !Container.IsExists<T>() || Container.GetSingle<T>() is MonoBehaviour monoBehaviour && !monoBehaviour;

            if (!isLoaded)
                return Container.GetSingle<T>();

            var popup = isInstantiate
                ? AssetProvider.Instantiate<T>(path, parent)
                : AssetProvider.Load<T>(path);

            Container.RegisterSingle(popup);

            return popup;
        }
    }
}
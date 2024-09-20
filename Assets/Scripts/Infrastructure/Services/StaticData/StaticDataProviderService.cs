using Configs;
using Infrastructure.AssetManagement;
using UnitonConnect.Core.Data;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataProviderService : IStaticDataProviderService
    {
        public GameConfig GameConfig { get; private set; }
        public WalletsProvidersData WalletsProvidersData { get; private set; }
        public void Load()
        {
            GameConfig = Resources.Load<GameConfig>(AssetPath.GameConfig);
            WalletsProvidersData = Resources.Load<WalletsProvidersData>(AssetPath.WalletsProvidersData);
        }
    }
}
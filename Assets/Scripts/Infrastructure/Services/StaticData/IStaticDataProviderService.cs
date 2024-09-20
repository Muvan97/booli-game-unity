using Configs;
using UnitonConnect.Core.Data;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataProviderService : IService
    {
        void Load();
        GameConfig GameConfig { get; }
        WalletsProvidersData WalletsProvidersData { get; }
    }
}
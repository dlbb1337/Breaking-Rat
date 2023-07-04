using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BreakingRat.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider
    {
        public bool Initialized { get; }

        public Task InitializeAsync();

        public Task<T> Load<T>(AssetReference assetReference) where T : class;

        public Task<T> Load<T>(string address) where T : class;

        public Task<T> InstantiateWithDI<T>(string path) where T : class;

        public Task<T> InstantiateWithDI<T>
            (string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : class;

        public Task<Object> Instantiate(string address, Transform parent = null);

        public Task<GameObject> Instantiate(string address, Vector3 at);

        public Task<GameObject> Instantiate(string address);

        public void Cleanup();
    }
}

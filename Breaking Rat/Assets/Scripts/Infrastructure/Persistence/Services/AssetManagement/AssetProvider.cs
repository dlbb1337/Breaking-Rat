using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCashe =
                new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
                new Dictionary<string, List<AsyncOperationHandle>>();
        private readonly IInstantiator _instantiator;

        public bool Initialized { get; private set; } = false;

        public AssetProvider(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public async Task InitializeAsync()
        {
            await Addressables.InitializeAsync().Task;
            Initialized = true;
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }

            if (_completedCashe.TryGetValue
                      (assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
              Addressables.LoadAssetAsync<T>(assetReference),
              cacheKey: assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }

            if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
              Addressables.LoadAssetAsync<T>(address),
              cacheKey: address);
        }


        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);

            _completedCashe.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>
                (AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _completedCashe[cacheKey] = completeHandle;
            };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>
                (string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        public async Task<GameObject> Instantiate(string address, Vector3 at)
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }
            return await Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;
        }

        public async Task<GameObject> Instantiate(string address)
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }

            return await Addressables.InstantiateAsync(address).Task;
        }

        public async Task<T> InstantiateWithDI<T>(string path) where T : class
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }
            var prefab = await Load<GameObject>(path);

            return _instantiator.InstantiatePrefabForComponent<T>(prefab);
        }

        public async Task<T> InstantiateWithDI<T>
            (string path,
             Vector3 position,
             Quaternion rotation,
             Transform parent = null) where T : class
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }

            GameObject prefab = await Load<GameObject>(path);

            return _instantiator.InstantiatePrefabForComponent<T>
                (prefab, position, rotation, parent);
        }

        public async Task<Object> Instantiate(string address, Transform parent = null)
        {
            if (Initialized == false)
            {
                await InitializeAsync();
            }
            return await Addressables.InstantiateAsync(address, parent).Task;
        }
    }
}




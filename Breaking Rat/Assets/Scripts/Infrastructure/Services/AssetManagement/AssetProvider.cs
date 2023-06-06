using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace BreakingRat.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IInstantiator _container;
        public AssetProvider(IInstantiator container)
        {
            _container = container;
        }

        public T InstantiateWithDI<T>
            (string path, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour
        {
            return _container.InstantiatePrefabResourceForComponent<T>(path, position, rotation, parent);
        }

        public GameObject InstantiateWithDI
            (string path, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return _container.InstantiatePrefabResource(path, position, rotation, parent);
        }

        public Object Instantiate
            (string path, Transform parent = null)
        {
            var prefab = Resources.Load(path);
            return Object.Instantiate(prefab, parent);
        }

        public T InstantiateWithDI<T>(string path, Transform parent = null) where T : MonoBehaviour
        {
            return _container.InstantiatePrefabResourceForComponent<T>(path, parent);
        }
    }
}

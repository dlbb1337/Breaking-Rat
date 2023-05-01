using BreakingRat.Services.Input;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace BreakingRat.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IInstantiator _container;
        public AssetProvider(IInstantiator container)
        {
            _container = container;
        }

        public T Instantiate<T>
            (string path, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour
        {
            return _container.InstantiatePrefabResourceForComponent<T>(path, position, rotation, parent);
        }
    }
}

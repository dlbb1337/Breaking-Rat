using BreakingRat.Infrastructure.Services.AssetManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BreakingRat.Assets.Scripts.Infrastructure.Services.AssetManagement
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedOperations = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _operations = new();

        public GameObject Load(AssetReference assetRef)
        {
            

            throw new System.NotImplementedException();
        }

        public Object Instantiate(string path, Transform parent = null)
        {
            throw new System.NotImplementedException();
        }

        public T InstantiateWithDI<T>(string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            throw new System.NotImplementedException();
        }

        public GameObject InstantiateWithDI(string path, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            throw new System.NotImplementedException();
        }

        public T InstantiateWithDI<T>(string path, Transform parent = null) where T : MonoBehaviour
        {
            throw new System.NotImplementedException();
        }
    }
}

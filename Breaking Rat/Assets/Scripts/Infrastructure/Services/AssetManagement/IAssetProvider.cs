using UnityEngine;

namespace BreakingRat.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider
    {
        public T InstantiateWithDI<T>
            (string path, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour;

        public GameObject InstantiateWithDI
            (string path, Vector3 position, Quaternion rotation, Transform parent = null);
        public T InstantiateWithDI<T>
            (string path, Transform parent = null)
            where T : MonoBehaviour;

        public Object Instantiate
            (string path,  Transform parent = null);

    }
}

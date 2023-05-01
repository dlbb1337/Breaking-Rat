using UnityEngine;

namespace BreakingRat.Services.AssetManagement
{
    public interface IAssetProvider
    {
        public T Instantiate<T>
            (string path, Vector3 position, Quaternion rotation, Transform parent = null)
            where T:MonoBehaviour;

    }
}

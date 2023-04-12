using UnityEngine;

namespace BreakingRat.Services
{
    public class AssetProvider : IAssetProvider
    {
        public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            var obj = Resources.Load<T>(path);
            return GameObject.Instantiate(obj, position, rotation, parent);
        }
    }
}

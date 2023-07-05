using UnityEngine;

namespace BreakingRat.Domain.Data
{
    [CreateAssetMenu(fileName = "MazesStaticData", menuName = "StaticData/MazesStaticData")]
    public class MazesStaticData : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private Vector3 _instantiateFirstMazePosition;

        public int Width => _width;
        public int Height => _height;
        public Vector3 InstantiateFirstMazePosition => _instantiateFirstMazePosition;
    }
}

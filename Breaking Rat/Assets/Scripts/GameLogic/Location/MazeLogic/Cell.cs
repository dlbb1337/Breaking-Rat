using UnityEngine;

namespace BreakingRat.GameLogic.Location.MazeLogic
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]  GameObject _leftWall;
        [SerializeField] private GameObject _bottomWall;

        public GameObject LeftWall => _leftWall;
        public GameObject BottomWall => _bottomWall;
    }
}

using BreakingRat.GameLogic.Location.MazeLogic;
using UnityEngine;
using Zenject;

namespace BreakingRat.GameLogic.DeathLogic
{
    public class Deadzone : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private Trigger _trigger;
        private MazeSpawner _mazeSpawner;

        public Trigger Trigger => _trigger;

        [Inject]
        private void Construct(MazeSpawner mazeSpawner)
        {
            _mazeSpawner = mazeSpawner;
        }

        private void Update()
        {
            transform.position += Vector3.up * (_moveSpeed + _speedMultiplier * _mazeSpawner.Score) * Time.deltaTime;
        }
    }
}

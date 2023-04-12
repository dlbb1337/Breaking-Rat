using BreakingRat.Location.MazeLogic;
using UnityEngine;

namespace BreakingRat.Infrastructure.States
{
    public class InitializeState : IState
    {
        private readonly MazeSpawner _mazeSpawner;

        public InitializeState(MazeSpawner mazeSpawner)
        {
            _mazeSpawner = mazeSpawner;
        }

        public void OnEnter()
        {
            _mazeSpawner.SpawnMaze(15, 15, new Vector3(-10, -10, 0));
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void OnStay()
        {
            throw new System.NotImplementedException();
        }
    }
}

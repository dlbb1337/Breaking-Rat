using BreakingRat.GameLogic;
using BreakingRat.GameLogic.DeathLogic;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.PlayerLogic;
using BreakingRat.Infrastructure.States;
using BreakingRat.Services.AssetManagement;
using BreakingRat.Services.Input;
using UnityEngine;
using Zenject;

namespace BreakingRat.Infrastructure
{
    public class FirstLevel : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private IAssetProvider _assetProvider;
        private MazeSpawner _mazeSpawner;
        private ITouchService _touchService;


        [Inject]
        private void Construct
            (GameStateMachine gameStateMachine,
            IAssetProvider assetProvider,
            MazeSpawner mazeSpawner,
            ITouchService touchService)
        {
            _gameStateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _mazeSpawner = mazeSpawner;
            _touchService = touchService;
        }

        private void Start()
        {
            _mazeSpawner.Capacity = 10;
            _mazeSpawner.SpawnMaze(12, 20, Vector3.zero);

            SpawnMazes();

            var player = _assetProvider.Instantiate<PlayerMovement>
                (AssetPaths.PlayerPrefabPath, new Vector3(6, 1, 0), Quaternion.identity);

            var follow = Camera.main.gameObject.AddComponent<Follow>();
            follow.Construct(player.transform, new Vector3(6, 4, -10), 1, true, false, false);
            _assetProvider.Instantiate<Deadzone>
                (AssetPaths.DeadzonePrefabPath, new Vector3(6, -30, 0), Quaternion.identity);

            _gameStateMachine.EnterState<GameLoopState>();
        }

        private void SpawnMazes()
        {
            for (int i = 0; i < 9; i++)
            {
                var lastMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];
                _mazeSpawner.SpawnMaze
                    (12, 20, lastMaze.transform.position + Vector3.up * 20, lastMaze.TemplateMaze.exit);
            }

            var upperMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];

            var position = upperMaze.transform.position + Vector3.up * 20;

            var templateMaze =
                _mazeSpawner.MazeGenerator.GenerateEmptyMaze(12, 20, upperMaze.TemplateMaze.exit);

            _mazeSpawner.SpawnMaze(12, 20, position, templateMaze);
        }
    }
}

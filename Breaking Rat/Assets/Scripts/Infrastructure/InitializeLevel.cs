using BreakingRat.Data;
using BreakingRat.Data.Obstacles;
using BreakingRat.Data.Services;
using BreakingRat.GameLogic.DeathLogic;
using BreakingRat.GameLogic.DeathLogic.Services;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.PlayerLogic;
using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.Services.Ads;
using BreakingRat.Infrastructure.States;
using BreakingRat.UI;
using System.Threading.Tasks;
using UnityEngine;
using IFactory = BreakingRat.Infrastructure.Factory.IFactory;

namespace BreakingRat.GameLogic
{
    public class InitializeLevel
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MazeSpawner _mazeSpawner;
        private readonly MazeGenerator _mazeGenerator;
        private readonly IStaticDataService _staticDataService;
        private readonly IFactory _factory;
        private readonly IDeathService _deathService;
        private readonly IAdsService _adsService;

        public InitializeLevel
            (GameStateMachine gameStateMachine,
            MazeSpawner mazeSpawner,
            MazeGenerator mazeGenerator,
            IStaticDataService staticDataService,
            IFactory factory,
            IDeathService deathService,
            IAdsService adsService)
        {
            _gameStateMachine = gameStateMachine;
            _mazeSpawner = mazeSpawner;
            _mazeGenerator = mazeGenerator;
            _staticDataService = staticDataService;
            _factory = factory;
            _deathService = deathService;
            _adsService = adsService;

            InitAsync();
        }


        private async Task InitAsync()
        {
            _gameStateMachine.EnterState<InitializeState>();

            var lvl = _staticDataService.CurrentLevelStaticData;

            var player = await PlayerAsync(lvl.PlayerStaticData);

            var deadzone = await DeadzoneAsync(lvl.DeadzoneStaticData);

            await CreateHUD(player, deadzone);

            await MazeSpawnerAsync(lvl.MazesStaticData);

            _gameStateMachine.EnterState<GameLoopState>();
        }

        private async Task CreateHUD(PlayerMovement player, Deadzone deadzone)
        {
            var HUD = await _factory.CreateHUD();

            HUD.Player = player.transform;
            HUD.Deadzone = deadzone.transform;
        }

        private async Task<PlayerMovement> PlayerAsync(PlayerStaticData data)
        {
            var player = await _factory.CreatePlayer
                (position: data.InstantiatePlayerPosition,
                 rotation: Quaternion.identity);

            player.MovementSpeed = data.PlayerMovementSpeed;
            player.TurnPercentage = data.TurnPercentage;

            var follow = Camera.main.gameObject.AddComponent<Follow>();
            Camera.main.orthographicSize = data.CameraSize;
            follow.Construct
                (player.transform,
                data.CameraOffset,
                data.CameraDefaultPosition,
                data.InterpolatePercentage,
                data.X,
                data.Y,
                data.Z);

            return player;
        }

        private async Task MazeSpawnerAsync(MazesStaticData data)
        {
            _mazeSpawner.LevelId = _staticDataService.LevelId;
            _mazeSpawner.Capacity = 20;
            await _mazeSpawner.SpawnMazeAsync(data.Width, data.Height, data.InstantiateFirstMazePosition);

            await SpawnMazesAsync(data);
        }

        private async Task<Deadzone> DeadzoneAsync(DeadzoneStaticData data)
        {
            var deadzone = await _factory.CreateDeadzone(data.InstantiateDeadzonePosition, Quaternion.identity);

            deadzone.MovementSpeed = data.MovementSpeed;
            deadzone.SpeedMultiplier = data.SpeedMultiplier;
            deadzone.Trigger.Enter.AddListener(collider => _deathService.Death());

            return deadzone;
        }

        private async Task SpawnMazesAsync(MazesStaticData data)
        {
            await InstantiatingMazesAsync(data);
        }

        private void InstantiateLastMaze(MazesStaticData data)
        {
            var upperMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count -3];

            upperMaze.ExitTrigger.Enter.AddListener(async collider => await InstantiatingMazesAsync(data));
        }

        private async Task InstantiatingMazesAsync(MazesStaticData data)
        {
            for (int i = 0; i < 15; i++)
            {
                var lastMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];
                await _mazeSpawner.SpawnMazeAsync
                    (data.Width, data.Height, lastMaze.transform.position + Vector3.up * data.Height, lastMaze.TemplateMaze.exit);
            }
            InstantiateLastMaze(data);
        }
    }
}

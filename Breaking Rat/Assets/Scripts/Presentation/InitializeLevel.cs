using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.GameLogic;
using BreakingRat.Application.GameLogic.DeathLogic;
using BreakingRat.Application.GameLogic.PlayerLogic;
using BreakingRat.Application.Services;
using BreakingRat.Application.StateMachine;
using BreakingRat.Application.StateMachine.States;
using BreakingRat.Domain.Data;
using BreakingRat.Domain.Data.Obstacles;
using System.Threading.Tasks;
using UnityEngine;
using IFactory = BreakingRat.Application.Services.Factories.IFactory;

namespace BreakingRat.Presentation
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
        private readonly IPauseService _pauseService;

        public InitializeLevel
            (GameStateMachine gameStateMachine,
            MazeSpawner mazeSpawner,
            MazeGenerator mazeGenerator,
            IStaticDataService staticDataService,
            IFactory factory,
            IDeathService deathService,
            IAdsService adsService,
            IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _mazeSpawner = mazeSpawner;
            _mazeGenerator = mazeGenerator;
            _staticDataService = staticDataService;
            _factory = factory;
            _deathService = deathService;
            _adsService = adsService;
            _pauseService = pauseService;

            InitAsync();
        }


        private async Task InitAsync()
        {
            _pauseService.PauseGame();

            _gameStateMachine.EnterState<InitializeState>();

            if (_staticDataService.Initialized == false)
            {
                await _staticDataService.InitializeAsync();
            }

            var lvl = _staticDataService.CurrentLevelStaticData;

            var player = await PlayerAsync(lvl.PlayerStaticData);

            var deadzone = await DeadzoneAsync(lvl.DeadzoneStaticData);

            await CreateHUD(player, deadzone);

            await MazeSpawnerAsync(lvl.MazesStaticData);

            _pauseService.UnPauseGame();

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
            await _mazeSpawner.SpawnMazeAsync
                (data.Width,
                 data.Height,
                 data.InstantiateFirstMazePosition);

            await SpawnMazesAsync(data);
        }

        private async Task<Deadzone> DeadzoneAsync(DeadzoneStaticData data)
        {
            var deadzone = await _factory.CreateDeadzone
                (data.InstantiateDeadzonePosition,rotation: Quaternion.identity);

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
            var upperMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 3];

            upperMaze.ExitTrigger.Enter.AddListener
                (async collider => await InstantiatingMazesAsync(data));
        }

        private async Task InstantiatingMazesAsync(MazesStaticData data)
        {
            for (int i = 0; i < 15; i++)
            {
                var lastMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];
                await _mazeSpawner.SpawnMazeAsync
                    (width: data.Width,
                     height: data.Height,
                     mazePosition: lastMaze.transform.position + Vector3.up * data.Height,
                     entry: lastMaze.TemplateMaze.exit);
            }
            InstantiateLastMaze(data);
        }
    }
}

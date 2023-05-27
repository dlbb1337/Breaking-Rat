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
using GameConsole.CommandTools;
using GameConsole.ConsoleManager;
using System.Collections.Generic;
using UnityEngine;
using IFactory = BreakingRat.Infrastructure.Factory.IFactory;

namespace BreakingRat.GameLogic
{
    public class InitializeLevel
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MazeSpawner _mazeSpawner;
        private readonly MazeGenerator _mazeGenerator;
        private readonly ConsoleService _consoleService;
        private readonly IStaticDataService _staticDataService;
        private readonly IFactory _factory;
        private readonly IDeathService _deathService;
        private readonly IAdsService _adsService;

        public InitializeLevel
            (GameStateMachine gameStateMachine,
            MazeSpawner mazeSpawner,
            MazeGenerator mazeGenerator,
            ConsoleService consoleService,
            List<ICommandContainer> containers,
            IStaticDataService staticDataService,
            IFactory factory,
            IDeathService deathService,
            IAdsService adsService)
        {
            _gameStateMachine = gameStateMachine;
            _mazeSpawner = mazeSpawner;
            _mazeGenerator = mazeGenerator;
            _consoleService = consoleService;
            _consoleService.AddCommands(containers);
            _staticDataService = staticDataService;
            _factory = factory;
            _deathService = deathService;
            _adsService = adsService;

            Init();
        }


        private void Init()
        {
            _gameStateMachine.EnterState<InitializeState>();

            var lvl = _staticDataService.CurrentLevelStaticData;

            var player = Player(lvl.PlayerStaticData);

            var deadzone = Deadzone(lvl.DeadzoneStaticData);

            CreateHUD(player, deadzone);

            MazeSpawner(lvl.MazesStaticData);

            _gameStateMachine.EnterState<GameLoopState>();
        }

        private void CreateHUD(PlayerMovement player, Deadzone deadzone)
        {
            var HUD = _factory.CreateHUD();

            HUD.Player = player.transform;
            HUD.Deadzone = deadzone.transform;
        }

        private PlayerMovement Player(PlayerStaticData data)
        {
            var player = _factory.CreatePlayer(data.InstantiatePlayerPosition,Quaternion.identity);

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

        private void MazeSpawner(MazesStaticData data)
        {
            _mazeSpawner.LevelId = _staticDataService.LevelId;
            _mazeSpawner.Capacity = 10;
            _mazeSpawner.SpawnMaze(data.Width, data.Height, data.InstantiateFirstMazePosition);

            SpawnMazes(data);
        }

        private Deadzone Deadzone(DeadzoneStaticData data)
        {
            var deadzone = _factory.CreateDeadzone(data.InstantiateDeadzonePosition, Quaternion.identity);

            deadzone.MovementSpeed = data.MovementSpeed;
            deadzone.SpeedMultiplier = data.SpeedMultiplier;
            deadzone.Trigger.Enter.AddListener(collider => _deathService.Death());

            return deadzone;
        }

        private void SpawnMazes(MazesStaticData data)
        {
            InstantiatingMazes(data);
            InstantiateLastMaze(data);
        }

        private void InstantiateLastMaze(MazesStaticData data)
        {
            var upperMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];

            var position = upperMaze.transform.position + Vector3.up * data.Height;

            var templateMaze =
                _mazeGenerator.GenerateEmptyMaze(data.Width, data.Height, upperMaze.TemplateMaze.exit);

            _mazeSpawner.SpawnMaze(templateMaze, position);
        }

        private void InstantiatingMazes(MazesStaticData data)
        {
            for (int i = 0; i < 9; i++)
            {
                var lastMaze = _mazeSpawner.Mazes[_mazeSpawner.Mazes.Count - 1];
                _mazeSpawner.SpawnMaze
                    (data.Width, data.Height, lastMaze.transform.position + Vector3.up * data.Height, lastMaze.TemplateMaze.exit);
            }
        }
    }
}

using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using BreakingRat.Location.MazeLogic;
using BreakingRat.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.CompositionRoot
{
    public class InfrastructureDependencies
    {
        private MazeSpawner _mazeSpawner;
        public GameStateMachine Machine { get; private set; }

        public InfrastructureDependencies()
        {
            _mazeSpawner = new MazeSpawner(new(),new AssetProvider());
            var states = new Dictionary<Type, IState>()
            {
                [typeof(InitializeState)] = new InitializeState( _mazeSpawner),
            };

            Machine = new GameStateMachine(states);
        }
    }
}

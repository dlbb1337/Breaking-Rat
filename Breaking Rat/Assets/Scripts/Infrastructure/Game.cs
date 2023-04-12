using BreakingRat.CompositionRoot;
using BreakingRat.Infrastructure.States;
using UnityEngine;

namespace BreakingRat.Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine { get; private set; }

        public Game(GameStateMachine gameStateMachine)
        {

            GameStateMachine = gameStateMachine;

            GameStateMachine.EnterState<InitializeState>();
        }
    }
}

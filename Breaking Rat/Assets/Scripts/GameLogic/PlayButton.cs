using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

namespace BreakingRat.GameLogic
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _button.onClick.AddListener(() => _gameStateMachine.EnterState<LoadLevelState, Action>(null));
        }
    }
}

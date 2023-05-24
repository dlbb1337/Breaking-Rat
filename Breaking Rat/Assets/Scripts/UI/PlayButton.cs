using BreakingRat.Data.Services;
using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _button.onClick.AddListener(() => _gameStateMachine.EnterState<LoadSceneState, string>("SampleScene"));
        }
    }
}

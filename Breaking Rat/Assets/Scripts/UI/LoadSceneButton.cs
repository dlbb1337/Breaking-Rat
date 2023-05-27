using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _sceneName;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _button.onClick.AddListener(() => _gameStateMachine.EnterState<LoadSceneState, string>(_sceneName));
        }
    }
}

using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Core.Application.StateMachine;
using BreakingRat.Assets.Scripts.Core.Application.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.Assets.Scripts.Core.Application.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _sceneName;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;

            _button.onClick.AddListener(() =>
            {
                assetProvider.Cleanup();
                _gameStateMachine.EnterState<LoadSceneState, string>(_sceneName);
            });
        }
    }
}

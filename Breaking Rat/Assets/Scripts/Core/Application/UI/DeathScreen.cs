using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.StateMachine;
using BreakingRat.Application.StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.Application.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _scoreAndRecordText;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct
            (GameStateMachine gameStateMachine,
             IScoreService scoreService,
             IRecordService recordService,
             IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;

            _scoreAndRecordText.text = $"Score: {scoreService.Score} \nRecord: {recordService.Record}";

            _playAgainButton.onClick.AddListener(() =>
            {
                assetProvider.Cleanup();

                _gameStateMachine.EnterState<LoadSceneState, string>("Level");
            });

            _menuButton.onClick.AddListener(() =>
            {
                assetProvider.Cleanup();

                _gameStateMachine.EnterState<LoadSceneState, string>("Menu");
            });
        }
    }
}

using BreakingRat.GameLogic.Services;
using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.GameLogic.DeathLogic
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _scoreAndRecordText;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IScoreService scoreService, IRecordService recordService)
        {
            _gameStateMachine = gameStateMachine;

            _scoreAndRecordText.text = $"Score: {scoreService.Score} \nRecord: {recordService.Record}";

            _playAgainButton.onClick.AddListener(() => _gameStateMachine.EnterState<LoadSceneState, string>("Level"));
            _menuButton.onClick.AddListener(() => _gameStateMachine.EnterState<LoadSceneState, string>("Menu"));
        }
    }
}

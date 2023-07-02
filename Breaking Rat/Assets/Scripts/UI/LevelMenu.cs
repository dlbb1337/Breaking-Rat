using BreakingRat.Data;
using BreakingRat.Data.Services;
using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using BreakingRat.UI.Factory;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.UI
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private Transform _display;
        [SerializeField] private Transform _content;
        [SerializeField] private TMP_Text _levelText;
        private Dictionary<Button, LevelStaticData> _levels = new();
        private IStaticDataService _staticDataService;
        private IUIFactory _factory;
        private IProgressService _progressService;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(
            IStaticDataService staticDataService,
            IUIFactory UIFactory,
            IProgressService progressService,
            GameStateMachine stateMachine)
        {
            _staticDataService = staticDataService;
            _factory = UIFactory;
            _progressService = progressService;
            _stateMachine = stateMachine;

            LoadLevelsAsync();
        }

        public async Task LoadLevelsAsync()
        {
            await _staticDataService.InitializeAsync();

            var datas = _staticDataService.LevelStaticDatas;

            foreach (var data in datas)
                await CreateLevelButtonAsync(data);

            DisableLevelsDisplay();

            UpdateLevelText(_staticDataService.CurrentLevelStaticData);

            _stateMachine.EnterState<GameLoopState>();
        }

        public void EnableLevelsDisplay() =>
            _display.gameObject.SetActive(true);

        public void DisableLevelsDisplay() =>
            _display.gameObject.SetActive(false);

        private async Task CreateLevelButtonAsync(LevelStaticData data)
        {
            var button = await _factory.CreateLevelButtonAsync(_content);

            button.onClick.AddListener(() => OnButtonClick(data));
            var text = button.GetComponentInChildren<TMP_Text>();
            text.text = $"Level {data.LevelId}";

            if (_levels.ContainsKey(button) == false)
                _levels[button] = data;
        }

        private void OnButtonClick(LevelStaticData data)
        {
            _staticDataService.SetLevelId(data.LevelId);
            UpdateLevelText(data);
            DisableLevelsDisplay();
        }

        private void UpdateLevelText(LevelStaticData data)
        {
            var records = _progressService.Progress.Records;
            var levelId = data.LevelId;

            if (records.ContainsKey(levelId) == false)
            {
                records[levelId] = 0;
            }

            _levelText.text = $"level: {levelId} \nrecord: {records[levelId]}";
        }
    }
}

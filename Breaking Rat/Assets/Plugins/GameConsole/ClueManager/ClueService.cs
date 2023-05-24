using GameConsole.CommandTools;
using GameConsole.ConsoleManager;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameConsole.ClueManager
{
    public class ClueService : MonoBehaviour
    {
        [SerializeField] private GameObject _cluePrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _display;
        private ConsoleInput _consoleInput;
        private CommandService _commandService;
        private List<string> _commandNames;
        private List<string> _similarCommands;
        private Dictionary<string, GameObject> _clues = new();
        private bool DisplayEnabled => _display.activeSelf == true;

        [Inject]
        private void Construct(ConsoleInput consoleInput, CommandService commandService)
        {
            _consoleInput = consoleInput;
            _commandService = commandService;
            _consoleInput.TextChanged += OnInputTextChanged;

            Init();
        }

        private void Init()
        {
            _commandNames = HandleCommandNames(GetNamesFromCommandService());

            CreateClues(_commandNames);

            if (DisplayEnabled)
                _display.SetActive(false);

        }

        private void OnInputTextChanged(string text)
        {
            DisableAllClues();

            if (text == string.Empty)
            {
                if (DisplayEnabled)
                    _display.SetActive(false);

                _similarCommands = null;
            }
            else
            {
                _similarCommands = FindSimilarCommands
                    (text[text.Length - 1], text.Length - 1, _similarCommands is null ? _commandNames : _similarCommands);

                if (_similarCommands.Count == 0)
                    return;

                if (DisplayEnabled == false)
                    _display.SetActive(true);

                EnableClues(_similarCommands);
            }
        }

        public void UpdateCommands()
        {
            Init();
        }

        private void CreateClues(List<string> commandNames)
        {
            foreach (var commandName in commandNames)
            {
                if (_clues.ContainsKey(commandName) == false)
                {
                    var clue = CreateClue();

                    HandleClue(commandName, clue);
                }
            }
        }

        private GameObject HandleClue(string commandName, GameObject clue)
        {
            clue.SetActive(false);

            var tmp_text = clue.GetComponentInChildren<TMP_Text>();

            var button = clue.GetComponent<Button>();

            tmp_text.text = commandName;

            button.onClick.AddListener(() => _consoleInput.Text = commandName.Replace("(", "").Replace(")", ""));

            _clues[commandName] = clue;

            return clue;
        }

        private List<string> HandleCommandNames(List<string> commandNames)
        {
            for (int i = 0; i < commandNames.Count; i++)
            {
                var parameters = _commandService.CommandsByName[commandNames[i]].GetParameters();

                var parametersInString =
                    string.Join("; ", parameters.Select(parameter => parameter.ParameterType.Name));


                commandNames[i] = commandNames[i] + " (" + parametersInString + ")";
            }
            return commandNames;
        }

        private List<string> GetNamesFromCommandService() =>
            _commandService.CommandsByName.Select(command => command.Key).ToList();

        private void EnableClues(List<string> keys)
        {
            foreach (var key in keys)
            {
                _clues[key].SetActive(true);
            }
        }

        private void DisableAllClues()
        {
            foreach (var clue in _clues)
            {
                if (clue.Value.activeSelf == true)
                    clue.Value.SetActive(false);
            }
        }

        private List<string> FindSimilarCommands(char letter, int index, List<string> strings) =>
            strings.Where(str => str[index] == letter).ToList();

        private GameObject CreateClue() =>
            Instantiate(_cluePrefab, _parent);
    }
}

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GameConsole.ConsoleManager;
using GameConsole.CommandTools;

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

        [Inject]
        private void Construct(ConsoleInput consoleInput, CommandService commandService)
        {
            _consoleInput = consoleInput;
            _commandService = commandService;

            _consoleInput.TextChanged += OnInputTextChanged;

            _commandNames = _commandService.CommandsByName.Select(command => command.Key).ToList();

            for (int i = 0; i < _commandNames.Count; i++)
            {
                var parameters = _commandService.CommandsByName[_commandNames[i]].GetParameters();

                var parametersInString =
                    string.Join("; ", parameters.Select(parameter => parameter.ParameterType.Name));


                _commandNames[i] = _commandNames[i] + " (" + parametersInString + ")";
            }

            foreach (var commandName in _commandNames)
            {
                var clue = CreateClue();
                clue.SetActive(false);

                var tmp_text = clue.GetComponentInChildren<TMP_Text>();

                var button = clue.GetComponent<Button>();

                tmp_text.text = commandName;

                button.onClick.AddListener(() => _consoleInput.Text = commandName.Replace("(", "").Replace(")", ""));

                if (_clues.ContainsKey(commandName) == false)
                    _clues[commandName] = clue;
            }

            if (_display.activeSelf == true)
                _display.SetActive(false);
        }

        private void OnInputTextChanged(string text)
        {
            foreach (var clue in _clues)
            {
                if (clue.Value.activeSelf == true)
                    clue.Value.SetActive(false);
            }

            if (text == string.Empty)
            {
                if (_display.activeSelf == true)
                    _display.SetActive(false);

                _similarCommands = null;
            }
            else
            {
                _similarCommands = FindSimilarCommands
                    (text[text.Length - 1], text.Length - 1, _similarCommands is null ? _commandNames : _similarCommands);

                if (_similarCommands.Count == 0)
                {
                    return;
                }

                if (_display.activeSelf == false)
                    _display.SetActive(true);

                foreach (var command in _similarCommands)
                {
                    _clues[command].SetActive(true);
                }
            }
        }

        private List<string> FindSimilarCommands(char letter, int index, List<string> strings)
        {
            return strings.Where(str => str[index] == letter).ToList();
        }

        private GameObject CreateClue() => Instantiate(_cluePrefab, _parent);
    }
}

using GameConsole.ClueManager;
using GameConsole.CommandTools;
using GameConsole.LogManager;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ILogHandler = GameConsole.LogManager.ILogHandler;

namespace GameConsole.ConsoleManager
{
    public class ConsoleService : MonoBehaviour
    {
        [SerializeField] private KeyCode _toggleKey;
        [SerializeField] private GameObject _consoleDisplay;
        private LogReceiver _logReceiver;
        private ILogHandler _logHandler;
        private MethodParser _methodParser;
        private CommandService _commandService;
        private ConsoleInput _consoleInput;
        private ConsoleOutput _consoleOutput;
        private ClueService _clueService;

        [Inject]
        private void Construct
            (LogReceiver logReceiver,
            ILogHandler logHandler,
            MethodParser methodParser,
            CommandService commandService,
            ConsoleInput consoleInput,
            ConsoleOutput consoleOutput,
            ClueService clueService)
        {
            _logHandler = logHandler;
            _logReceiver = logReceiver;
            _methodParser = methodParser;
            _commandService = commandService;
            _consoleInput = consoleInput;
            _consoleOutput = consoleOutput;
            _clueService = clueService;
        }

        private void OnEnable()
        {
            _logReceiver.MessageReceived += OnLogReceived;
            _consoleInput.Submit += OnInputSubmit;

            if (_consoleDisplay.activeSelf)
                _consoleDisplay.SetActive(false);
        }

        private void OnDisable()
        {
            _logReceiver.MessageReceived -= OnLogReceived;
            _consoleInput.Submit -= OnInputSubmit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_toggleKey))
                _consoleDisplay.SetActive(_consoleDisplay.activeSelf ? false : true);
        }

        private void OnInputSubmit(string text)
        {
            var method = text.ToLower().Split(' ');
            var methodInfo = _commandService.FindMethodByName(method[0]);

            if (methodInfo is null)
                return;

            var parameters = _methodParser.ParseParameters(methodInfo, text.Substring(method[0].Length));
            _commandService.RunCommand(methodInfo, parameters);
        }

        private void OnLogReceived(Log receivedLog)
        {
            var processedLog = _logHandler.HandleLog(receivedLog);

            _consoleOutput.DisplayLog(processedLog);
        }

        public void AddCommands(List<ICommandContainer> commands)
        {
            _commandService.AddCommands(commands);
            _clueService.UpdateCommands();
        }
    }
}


using UnityEngine;
using Zenject;
using GameConsole.LogManager;
using ILogHandler = GameConsole.LogManager.ILogHandler;
using GameConsole.CommandTools;

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

        [Inject]
        private void Construct
            (LogReceiver logReceiver,
            ILogHandler logHandler,
            MethodParser methodParser,
            CommandService commandService,
            ConsoleInput consoleInput,
            ConsoleOutput consoleOutput)
        {
            _logHandler = logHandler;
            _logReceiver = logReceiver;
            _methodParser = methodParser;
            _commandService = commandService;
            _consoleInput = consoleInput;
            _consoleOutput = consoleOutput;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            _logReceiver.MessageReceived += OnLogReceived;
            _consoleInput.Submit += OnInputSubmit;
        }

        private void OnDisable()
        {
            _logReceiver.MessageReceived -= OnLogReceived;
            _consoleInput.Submit -= OnInputSubmit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_toggleKey))
            {
                _consoleDisplay.SetActive(_consoleDisplay.activeSelf ? false : true);
            }
        }

        private void OnInputSubmit(string text)
        {
            var method = text.ToLower().Split(' ');
            var methodInfo = _commandService.FindMethodByName(method[0]);

            if (methodInfo is null)
            {
                return;
            }

            var parameters = _methodParser.ParseParameters(methodInfo, text.Substring(method[0].Length));
            _commandService.RunCommand(methodInfo, parameters);
        }

        private void OnLogReceived(Log receivedLog)
        {
            var processedLog = _logHandler.HandleLog(receivedLog);

            _consoleOutput.DisplayLog(processedLog);
        }
    }
}


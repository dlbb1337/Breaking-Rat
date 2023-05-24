using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameConsole.CommandTools
{
    public class CommandService
    {
        private readonly HashSet<ICommandContainer> _containers = new();
        private readonly Dictionary<MethodInfo, ICommandContainer> _commands = new();
        private readonly Dictionary<string, MethodInfo> _commandsByName = new();
        private readonly string[] _necessaryMethods = { "tostring", "equals", "gettype", "gethashcode" };

        public Dictionary<string, MethodInfo> CommandsByName => _commandsByName;

        public CommandService(List<ICommandContainer> containers)
        {
            _containers.UnionWith(containers);
            HandleContainers(containers.Except(_containers));
        }

        public void AddCommands(IEnumerable<ICommandContainer> containers)
        {
            HandleContainers(containers);
        }

        public MethodInfo FindMethodByName(string commandName)
        {
            if (_commandsByName.ContainsKey(commandName))
                return _commandsByName[commandName];

            return null;
        }

        public void RunCommand(MethodInfo command, params object[] parameters) =>
            command.Invoke(_commands[command], parameters);

        private void HandleContainers(IEnumerable<ICommandContainer> containers)
        {
            foreach (var container in containers)
            {
                HandleContainer(container);
            }

            HandleCommands();
        }

        private void HandleContainer(ICommandContainer container)
        {
            var methods = GetMethods(container);
            HandleMethods(container, methods);
        }

        private void HandleMethods(ICommandContainer container, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                if (_necessaryMethods.Contains(method.Name.ToLower()))
                    continue;

                if (_commands.ContainsKey(method) == false)
                    _commands[method] = container;
            }
        }

        private MethodInfo[] GetMethods(object container) =>
            container.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

        private void HandleCommands()
        {
            foreach (var command in _commands)
                if (_commandsByName.ContainsKey(command.Key.Name.ToLower()) == false)
                    _commandsByName[command.Key.Name.ToLower()] = command.Key;
        }
    }
}

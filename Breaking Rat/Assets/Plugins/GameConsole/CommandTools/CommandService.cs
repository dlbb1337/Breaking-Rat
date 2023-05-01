using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameConsole.CommandTools
{
    public class CommandService
    {
        private Dictionary<MethodInfo, ICommandContainer> _commands = new();
        private Dictionary<string, MethodInfo> _commandsByName = new();

        public Dictionary<string, MethodInfo> CommandsByName => _commandsByName;

        public CommandService(List<ICommandContainer> containers)
        {
            string[] necessaryMethods = { "tostring", "equals", "gettype", "gethashcode" };

            foreach (var container in containers)
            {
                var methods = container.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (var method in methods)
                {
                    if (necessaryMethods.Contains(method.Name.ToLower()))
                        continue;

                    if (_commands.ContainsKey(method) == false)
                        _commands[method] = container;
                }
            }

            foreach (var command in _commands)
                if (_commandsByName.ContainsKey(command.Key.Name.ToLower()) == false)
                    _commandsByName[command.Key.Name.ToLower()] = command.Key;
        }

        public MethodInfo FindMethodByName(string commandName)
        {
            if (_commandsByName.ContainsKey(commandName))
                return _commandsByName[commandName];

            return null;
        }

        public void RunCommand(MethodInfo command, params object[] parameters)
        {
            command.Invoke(_commands[command], parameters);
        }
    }
}

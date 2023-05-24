using UnityEngine;
using Zenject;
using GameConsole.LogManager;
using GameConsole.ConsoleManager;
using GameConsole.CommandTools;
using GameConsole.CommandTools.Commands;
using GameConsole.ClueManager;

public class ConsoleInstaller : MonoInstaller
{
    [SerializeField] private ConsoleInput _consoleInput;
    [SerializeField] private ConsoleOutput _consoleOutput;
    [SerializeField] private ClueService _clueService;

    public override void InstallBindings()
    {
        Container.Bind<ConsoleInput>().FromInstance(_consoleInput).AsSingle();
        Container.Bind<ConsoleOutput>().FromInstance(_consoleOutput).AsSingle();
        Container.Bind<GameConsole.LogManager.ILogHandler>().FromInstance(new SimpleLogsHandler()).AsSingle();
        Container.Bind<LogReceiver>().FromInstance(new LogReceiver()).AsSingle();
        Container.Bind<ICommandContainer>().To<SimpleCommandContainer>().AsSingle();
        Container.Bind<CommandService>().FromNew().AsSingle();
        Container.Bind<MethodParser>().FromInstance(new MethodParser()).AsSingle();
        Container.Bind<ClueService>().FromInstance(_clueService).AsSingle();
        Container.Bind<ConsoleService>().FromInstance(this.GetComponent<ConsoleService>()).AsSingle();
    }
}
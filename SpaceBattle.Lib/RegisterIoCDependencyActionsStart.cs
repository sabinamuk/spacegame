using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        new RegisterIoCDependencyMacroCommand().Execute();
        new RegisterDependencyCommandInjectableCommand().Execute();
        new RegisterIoCDependencySendCommand().Execute();
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Actions.Start",
            (object[] args) => new StartCommand((IDictionary<string, object>)args[0])
        ).Execute();
    }
}

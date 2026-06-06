using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.CommandInjectable",
            (object[] _) => new CommandInjectableCommand()
        ).Execute();
    }
}

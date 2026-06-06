using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Macro", (object[] args) =>
        {
            var commands = (IEnumerable<ICommand>)args[0];
            return new MacroCommand(commands.ToList());
        }).Execute();
    }
}

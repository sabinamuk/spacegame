using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShootCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Shoot",
            (object[] args) => new ShootCommand((IDictionary<string, object>)args[0])
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Torpedo.NewId",
            (object[] _) => (object)Guid.NewGuid().ToString()
        ).Execute();
    }
}

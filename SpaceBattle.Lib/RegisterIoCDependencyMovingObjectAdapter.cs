using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMovingObjectAdapter : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject",
            (object[] args) => new MovingObjectAdapter((IDictionary<string, object>)args[0])
        ).Execute();
    }
}

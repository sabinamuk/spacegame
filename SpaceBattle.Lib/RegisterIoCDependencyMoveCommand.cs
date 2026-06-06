using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move", (object[] args) =>
        {
            var movable = Ioc.Resolve<IMovable>("Adapters.IMovingObject", args[0]);
            return new MoveCommand(movable);
        }).Execute();
    }
}

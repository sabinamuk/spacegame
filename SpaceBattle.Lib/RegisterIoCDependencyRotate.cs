using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Rotate", (object[] args) =>
        {
            var rotatingObject = Ioc.Resolve<IRotatingObject>("Adapters.IRotatingObject", args[0]);
            return new RotateCommand(rotatingObject);
        }).Execute();
    }
}

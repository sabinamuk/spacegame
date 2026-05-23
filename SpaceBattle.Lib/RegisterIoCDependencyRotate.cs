using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register(
            "Commands.Rotate",
            args =>
            {
                var obj =
                    Ioc.Resolve<IRotatingObject>(
                        "Adapters.IRotatingObject",
                        args[0]);

                return new RotateCommand(obj);
            });
    }
}

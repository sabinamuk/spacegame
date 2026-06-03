using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Rotate", args =>
        {
            var obj = args["obj"];

            var dict = new Dictionary<string, object>
            {
                { "obj", obj }
            };

            var rotatingObject =
                (IRotatingObject)
                Ioc.Resolve("Adapters.IRotatingObject", dict);

            return new RotateCommand(rotatingObject);
        });
    }
}

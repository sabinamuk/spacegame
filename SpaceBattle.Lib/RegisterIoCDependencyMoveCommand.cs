using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Move", args =>
        {
            var obj = args["obj"];

            var dict = new Dictionary<string, object>
            {
                { "obj", obj }
            };

            var position = (IHasPosition)Ioc.Resolve("Adapters.IMovingObject", dict);
            var velocity = (IHasVelocity)Ioc.Resolve("Adapters.IMovingObject", dict);

            return new MoveCommand(position, velocity);
        });
    }
}

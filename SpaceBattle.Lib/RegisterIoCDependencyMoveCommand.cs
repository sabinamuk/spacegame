using System.Collections.Generic;

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

            var movable =
                (IMovable)Ioc.Resolve(
                    "Adapters.IMovingObject",
                    dict);

            return new MoveCommand(movable);
        });
    }
}

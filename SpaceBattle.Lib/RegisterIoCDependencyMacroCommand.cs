using SpaceBattle.Lib;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register(
            "Commands.Macro",
            args =>
            {
                var commands =
                    (IEnumerable<ICommand>)args["commands"];

                return new MacroCommand(commands.ToList());
            });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy
{
    private readonly string commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        this.commandSpec = commandSpec;
    }

    public ICommand Resolve(object[] args)
    {
        var commandNames =
            (IEnumerable<string>)
            Ioc.Resolve("Specs." + commandSpec);

        var commands =
            commandNames
                .Select(name =>
                    (ICommand)Ioc.Resolve(name, args))
                .ToList();

        return new MacroCommand(commands);
    }
}

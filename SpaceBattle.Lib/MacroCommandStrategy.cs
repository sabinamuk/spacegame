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
        var emptyArgs = new Dictionary<string, object>();

        var commandNames =
            (IEnumerable<string>)
            Ioc.Resolve("Specs." + commandSpec, emptyArgs);

        var dictArgs = argsToDict(args);

        var commands =
            commandNames
                .Select(name =>
                    (ICommand)Ioc.Resolve(name, dictArgs))
                .ToList();

        return new MacroCommand(commands);
    }

    private Dictionary<string, object> argsToDict(object[] args)
    {
        var dict = new Dictionary<string, object>();

        if (args == null || args.Length == 0)
            return dict;

        for (int i = 0; i < args.Length - 1; i += 2)
        {
            dict[(string)args[i]] = args[i + 1];
        }

        return dict;
    }
}

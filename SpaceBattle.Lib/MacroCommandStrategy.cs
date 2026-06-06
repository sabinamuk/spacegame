using Ioc = App.Ioc;

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
        var commandNames = Ioc.Resolve<IEnumerable<string>>("Specs." + commandSpec);

        var commands = commandNames
            .Select(name => Ioc.Resolve<ICommand>(name, args))
            .ToList();

        return new MacroCommand(commands);
    }
}

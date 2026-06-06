namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private readonly IEnumerable<ICommand> commands;

    public MacroCommand(IEnumerable<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        commands.ToList().ForEach(c => c.Execute());
    }
}

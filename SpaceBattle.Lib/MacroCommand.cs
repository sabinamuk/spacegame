using System;
using System.Linq;
using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private readonly ICommand[] commands;

    public MacroCommand(ICommand[] commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        ExecuteRecursive(0);
    }

    private void ExecuteRecursive(int index)
    {
        if (index >= commands.Length)
            return;

        commands[index].Execute();

        ExecuteRecursive(index + 1);
    }
}

using Moq;
using Xunit;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MacroCommandTests
{
    [Fact]
public void Execute_AllCommands_AreExecuted()
{
    var cmd1 = new Mock<ICommand>();
    var cmd2 = new Mock<ICommand>();

    var macro = new MacroCommand(
        new List<ICommand>
        {
            cmd1.Object,
            cmd2.Object
        });

    macro.Execute();

    cmd1.Verify(x => x.Execute(), Times.Once);
    cmd2.Verify(x => x.Execute(), Times.Once);
}
}

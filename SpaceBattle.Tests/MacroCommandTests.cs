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
            new ICommand[]
            {
                cmd1.Object,
                cmd2.Object
            });

        macro.Execute();

        cmd1.Verify(x => x.Execute(), Times.Once);
        cmd2.Verify(x => x.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_Throws_AndStops_When_CommandFails()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        var cmd3 = new Mock<ICommand>();

        cmd2
            .Setup(x => x.Execute())
            .Throws(new Exception());

        var macro = new MacroCommand(
            new ICommand[]
            {
                cmd1.Object,
                cmd2.Object,
                cmd3.Object
            });

        Assert.Throws<Exception>(
            () => macro.Execute());

        cmd1.Verify(x => x.Execute(), Times.Once);

        cmd3.Verify(
            x => x.Execute(),
            Times.Never);
    }
}
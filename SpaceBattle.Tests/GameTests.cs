using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class GameTests
{
    [Fact]
    public void Update_ExecutesOneCommand()
    {
        var game = new Game();
        var cmd = new Mock<ICommand>();
        game.Receive(cmd.Object);

        game.Update();

        cmd.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Update_ProcessesCommandsOneAtATime()
    {
        var game = new Game();
        var first = new Mock<ICommand>();
        var second = new Mock<ICommand>();
        game.Receive(first.Object);
        game.Receive(second.Object);

        game.Update();

        first.Verify(c => c.Execute(), Times.Once);
        second.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void Update_DoesNothingWhenQueueIsEmpty()
    {
        var game = new Game();

        var ex = Record.Exception(() => game.Update());

        Assert.Null(ex);
    }
}

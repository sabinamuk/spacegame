using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CommandInjectableCommand_test
{
    [Fact]
    public void Execute_CallsInjectedCommand_WhenCommandIsInjected()
    {
        // Arrange
        var commandMock = new Mock<ICommand>();
        var sut = new CommandInjectableCommand();

        // Act
        sut.Inject(commandMock.Object);

        sut.Execute();

        // Assert
        commandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_ThrowsException_WhenNoCommandInjected()
    {
        // Arrange
        var sut = new CommandInjectableCommand();

        // Act + Assert
        Assert.Throws<Exception>(() => sut.Execute());
    }
}

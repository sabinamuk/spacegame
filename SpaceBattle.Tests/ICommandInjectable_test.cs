using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class ICommandInjectable_test
{
    [Fact]
    public void Inject_CallsInjectWithCorrectCommand()
    {
        var command = new Mock<ICommand>();
        var injectable = new Mock<ICommandInjectable>();

        injectable.Object.Inject(command.Object);

        injectable.Verify(i => i.Inject(command.Object), Times.Once);
    }
}

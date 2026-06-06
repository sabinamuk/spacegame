using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class StopCommand_test
{
    public StopCommand_test()
    {
        new RegisterIoCDependencyActionsStop().Execute();
    }

    [Fact]
    public void Execute_InjectsEmptyCommandIntoInjectable()
    {
        var injectable = new Mock<ICommandInjectable>();
        var gameObject = new Dictionary<string, object>
        {
            ["repeatableMove"] = injectable.Object
        };

        var order = new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["gameObject"] = gameObject
        };

        ((ICommand)Ioc.Resolve("Actions.Stop", order)).Execute();

        injectable.Verify(i => i.Inject(It.IsAny<EmptyCommand>()), Times.Once);
    }

    [Fact]
    public void Execute_ThrowsIfOperationNotStarted()
    {
        var gameObject = new Dictionary<string, object>();

        var order = new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["gameObject"] = gameObject
        };

        Assert.Throws<KeyNotFoundException>(() =>
            ((ICommand)Ioc.Resolve("Actions.Stop", order)).Execute());
    }
}

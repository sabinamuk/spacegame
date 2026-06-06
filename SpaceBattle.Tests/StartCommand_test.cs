using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class StartCommand_test
{
    public StartCommand_test()
    {
        new RegisterIoCDependencyActionsStart().Execute();

        var moveCmd = new Mock<ICommand>();
        Ioc.Register("Specs.Move", _ => (object)new List<string> { "Commands.Move" });
        Ioc.Register("Commands.Move", _ => moveCmd.Object);
    }

    [Fact]
    public void Execute_StoresInjectableInGameObject()
    {
        var gameObject = new Dictionary<string, object>();
        var queue = new Mock<ICommandReceiver>();

        var order = new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["queue"] = queue.Object,
            ["gameObject"] = gameObject
        };

        ((ICommand)Ioc.Resolve("Actions.Start", order)).Execute();

        Assert.True(gameObject.ContainsKey("repeatableMove"));
        Assert.IsType<CommandInjectableCommand>(gameObject["repeatableMove"]);
    }
}

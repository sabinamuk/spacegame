using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class ActionsStartStop_test
{
    public ActionsStartStop_test()
    {
        new RegisterIoCDependencyActionsStart().Execute();
        new RegisterIoCDependencyActionsStop().Execute();

        var moveCmd = new Mock<ICommand>();
        Ioc.Register("Specs.Move", _ => (object)new List<string> { "Commands.Move" });
        Ioc.Register("Commands.Move", _ => moveCmd.Object);
    }

    [Fact]
    public void Stop_InjectsEmptyCommand_ForCorrectShip()
    {
        var ship1 = new Dictionary<string, object>();
        var ship2 = new Dictionary<string, object>();
        var queue = new Mock<ICommandReceiver>();

        ((ICommand)Ioc.Resolve("Actions.Start", new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["queue"]         = queue.Object,
            ["gameObject"]    = ship1
        })).Execute();

        ((ICommand)Ioc.Resolve("Actions.Start", new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["queue"]         = queue.Object,
            ["gameObject"]    = ship2
        })).Execute();

        ((ICommand)Ioc.Resolve("Actions.Stop", new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["gameObject"]    = ship1
        })).Execute();

        var injectable1 = (CommandInjectableCommand)ship1["repeatableMove"];
        var injectable2 = (CommandInjectableCommand)ship2["repeatableMove"];

        Assert.IsType<EmptyCommand>(GetInjected(injectable1));
        Assert.IsNotType<EmptyCommand>(GetInjected(injectable2));
    }

    private static ICommand GetInjected(CommandInjectableCommand c)
    {
        var field = typeof(CommandInjectableCommand)
            .GetField("_injected_command",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        return (ICommand)field.GetValue(c)!;
    }
}

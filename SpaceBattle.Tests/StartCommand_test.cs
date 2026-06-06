using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class StartCommand_test
{
    public StartCommand_test()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();

        new RegisterIoCDependencyActionsStart().Execute();

        var moveCmd = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Specs.Move",
            (object[] _) => (object)new List<string> { "Commands.Move" }
        ).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move",
            (object[] _) => moveCmd.Object
        ).Execute();
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

        Ioc.Resolve<ICommand>("Actions.Start", order).Execute();

        Assert.True(gameObject.ContainsKey("repeatableMove"));
        Assert.IsType<CommandInjectableCommand>(gameObject["repeatableMove"]);
    }
}

using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class StopCommand_test
{
    public StopCommand_test()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
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

        Ioc.Resolve<ICommand>("Actions.Stop", order).Execute();

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
            Ioc.Resolve<ICommand>("Actions.Stop", order).Execute());
    }
}

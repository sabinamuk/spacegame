using Ioc = App.Ioc;
using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void MoveDependency_IsRegistered()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject",
            (object[] _) => new TestAdapter()
        ).Execute();

        new RegisterIoCDependencyMoveCommand().Execute();

        var obj = new Mock<IMovingObject>().Object;
        var cmd = Ioc.Resolve<ICommand>("Commands.Move", obj);

        Assert.NotNull(cmd);
    }
}

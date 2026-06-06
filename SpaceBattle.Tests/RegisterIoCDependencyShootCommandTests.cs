using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyShootCommandTests
{
    public RegisterIoCDependencyShootCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void ShootDependency_IsRegistered()
    {
        new RegisterIoCDependencyShootCommand().Execute();

        var order = new Dictionary<string, object>
        {
            ["gameObjectId"] = "ship1",
            ["repository"] = new Mock<IGameObjectRepository>().Object,
            ["queue"] = new Mock<ICommandReceiver>().Object
        };
        var cmd = Ioc.Resolve<ICommand>("Commands.Shoot", order);

        Assert.NotNull(cmd);
    }
}

using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyActionsStop_test
{
    public RegisterIoCDependencyActionsStop_test()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
        new RegisterIoCDependencyActionsStop().Execute();
    }

    [Fact]
    public void Resolve_ReturnsICommand()
    {
        IDictionary<string, object> order = new Dictionary<string, object>();

        var result = Ioc.Resolve<ICommand>("Actions.Stop", order);

        Assert.IsAssignableFrom<ICommand>(result);
    }
}

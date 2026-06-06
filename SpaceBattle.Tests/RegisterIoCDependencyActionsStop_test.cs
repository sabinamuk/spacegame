using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyActionsStop_test
{
    public RegisterIoCDependencyActionsStop_test()
    {
        new RegisterIoCDependencyActionsStop().Execute();
    }

    [Fact]
    public void Resolve_ReturnsICommand()
    {
        IDictionary<string, object> order = new Dictionary<string, object>();

        var result = Ioc.Resolve("Actions.Stop", new Dictionary<string, object>(order));

        Assert.IsAssignableFrom<ICommand>(result);
    }
}

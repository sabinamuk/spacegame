using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyActionsStart_test
{
    public RegisterIoCDependencyActionsStart_test()
    {
        new RegisterIoCDependencyActionsStart().Execute();
    }

    [Fact]
    public void Resolve_ReturnsICommand()
    {
        IDictionary<string, object> order = new Dictionary<string, object>();

        var result = Ioc.Resolve("Actions.Start", new Dictionary<string, object>(order));

        Assert.IsAssignableFrom<ICommand>(result);
    }
}

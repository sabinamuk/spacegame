using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameObjectRepository_test
{
    public RegisterIoCDependencyGameObjectRepository_test()
    {
        new RegisterIoCDependencyGameObjectRepository().Execute();
    }

    [Fact]
    public void Resolve_ReturnsSameRepositoryInstance()
    {
        var r1 = Ioc.Resolve("Game.Objects.Repository", new Dictionary<string, object>());
        var r2 = Ioc.Resolve("Game.Objects.Repository", new Dictionary<string, object>());

        Assert.Same(r1, r2);
    }

    [Fact]
    public void Resolve_ReturnsIGameObjectRepository()
    {
        var result = Ioc.Resolve("Game.Objects.Repository", new Dictionary<string, object>());

        Assert.IsAssignableFrom<IGameObjectRepository>(result);
    }
}

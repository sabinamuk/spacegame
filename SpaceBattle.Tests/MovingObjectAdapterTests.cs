using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class MovingObjectAdapterTests
{
    private readonly Dictionary<string, object> _obj = new()
    {
        ["position"] = new Vector(3, 4),
        ["velocity"] = new Vector(1, 2)
    };

    [Fact]
    public void Position_ReturnsValueFromDictionary()
    {
        var adapter = new MovingObjectAdapter(_obj);

        Assert.Equal(new Vector(3, 4), adapter.Position);
    }

    [Fact]
    public void Velocity_ReturnsValueFromDictionary()
    {
        var adapter = new MovingObjectAdapter(_obj);

        Assert.Equal(new Vector(1, 2), adapter.Velocity);
    }

    [Fact]
    public void Position_Set_UpdatesDictionary()
    {
        var adapter = new MovingObjectAdapter(_obj);

        adapter.Position = new Vector(9, 0);

        Assert.Equal(new Vector(9, 0), _obj["position"]);
    }
}

public class RegisterIoCDependencyMovingObjectAdapterTests
{
    public RegisterIoCDependencyMovingObjectAdapterTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void Resolve_ReturnsMovingObjectAdapter()
    {
        new RegisterIoCDependencyMovingObjectAdapter().Execute();

        var obj = new Dictionary<string, object>
        {
            ["position"] = new Vector(0, 0),
            ["velocity"] = new Vector(1, 0)
        };

        var result = Ioc.Resolve<IMovable>("Adapters.IMovingObject", obj);

        Assert.IsType<MovingObjectAdapter>(result);
    }
}

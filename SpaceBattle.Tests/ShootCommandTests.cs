using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class ShootCommandTests
{
    private readonly GameObjectRepository _repository = new();
    private readonly Mock<ICommandReceiver> _queue = new();
    private readonly Vector _shipPosition = new(5, 3);
    private readonly Vector _shipVelocity = new(1, 0);

    public ShootCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();

        new RegisterIoCDependencyActionsStart().Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Specs.Move",
            (object[] _) => (object)new List<string> { "Commands.Move" }
        ).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move",
            (object[] _) => new Mock<ICommand>().Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject",
            (object[] args) =>
            {
                var dict = (IDictionary<string, object>)args[0];
                var mock = new Mock<IMovable>();
                mock.Setup(m => m.Position).Returns((Vector)dict["position"]);
                mock.Setup(m => m.Velocity).Returns((Vector)dict["velocity"]);
                return mock.Object;
            }
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Torpedo.NewId",
            (object[] _) => (object)"torpedo1"
        ).Execute();

        _repository.Set("ship1", new Dictionary<string, object>
        {
            ["position"] = _shipPosition,
            ["velocity"] = _shipVelocity
        });
    }

    private IDictionary<string, object> MakeOrder() => new Dictionary<string, object>
    {
        ["gameObjectId"] = "ship1",
        ["repository"] = _repository,
        ["queue"] = _queue.Object
    };

    [Fact]
    public void Execute_AddsTorpedoToRepository()
    {
        new ShootCommand(MakeOrder()).Execute();

        Assert.Equal(2, _repository.Count);
    }

    [Fact]
    public void Execute_TorpedoHasShipPosition()
    {
        new ShootCommand(MakeOrder()).Execute();

        var torpedo = _repository.Get("torpedo1");
        Assert.Equal(_shipPosition, torpedo["position"]);
    }

    [Fact]
    public void Execute_TorpedoHasShipVelocity()
    {
        new ShootCommand(MakeOrder()).Execute();

        var torpedo = _repository.Get("torpedo1");
        Assert.Equal(_shipVelocity, torpedo["velocity"]);
    }

    [Fact]
    public void Execute_SendsTorpedoToQueue()
    {
        new ShootCommand(MakeOrder()).Execute();

        _queue.Verify(q => q.Receive(It.IsAny<ICommand>()), Times.Once);
    }
}

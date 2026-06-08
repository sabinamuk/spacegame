using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCollisionTests
{
    public RegisterIoCDependencyCollisionTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void Execute_RegistersDetectorsCollision()
    {
        new RegisterIoCDependencyCollision(new CollisionMap(), new SpatialHash()).Execute();

        var detector = Ioc.Resolve<ICollisionDetector>("Detectors.Collision");

        Assert.IsType<CollisionDetector>(detector);
    }

    [Fact]
    public void Execute_RegistersCommandsCheckCollision()
    {
        new RegisterIoCDependencyCollision(new CollisionMap(), new SpatialHash()).Execute();

        var order = new Dictionary<string, object>
        {
            ["selfId"] = "ship-1",
            ["gameObject"] = new Dictionary<string, object>
            {
                ["shapeId"] = "ship",
                ["position"] = new Vector(0, 0),
                ["velocity"] = new Vector(0, 0)
            },
            ["onCollision"] = new Mock<ICommand>().Object
        };

        var cmd = Ioc.Resolve<ICommand>("Commands.CheckCollision", order);

        Assert.IsType<CheckCollisionCommand>(cmd);
    }

    [Fact]
    public void Execute_RegistersAdaptersICollidable()
    {
        new RegisterIoCDependencyCollision(new CollisionMap(), new SpatialHash()).Execute();

        var gameObject = new Dictionary<string, object>
        {
            ["shapeId"] = "ship",
            ["position"] = new Vector(1, 2),
            ["velocity"] = new Vector(0, 0)
        };

        var collidable = Ioc.Resolve<ICollidable>("Adapters.ICollidable", gameObject);

        Assert.Equal("ship", collidable.GetShapeId());
        Assert.Equal(new Vector(1, 2), collidable.GetPosition());
    }
}

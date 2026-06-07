using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCollision : ICommand
{
    private readonly ICollisionMap _map;
    private readonly ISpatialHash _spatialHash;

    public RegisterIoCDependencyCollision(ICollisionMap map, ISpatialHash spatialHash)
    {
        _map = map;
        _spatialHash = spatialHash;
    }

    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.Map",
            (object[] _) => _map
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Detectors.Collision",
            (object[] _) => new CollisionDetector(Ioc.Resolve<ICollisionMap>("Collision.Map"))
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Collision.SpatialHash",
            (object[] _) => _spatialHash
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.ICollidable",
            (object[] args) => new CollidableAdapter((IDictionary<string, object>)args[0])
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.CheckCollision",
            (object[] args) =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new CheckCollisionCommand(
                    (string)order["selfId"],
                    Ioc.Resolve<ICollidable>("Adapters.ICollidable", order["gameObject"]),
                    Ioc.Resolve<ISpatialHash>("Collision.SpatialHash"),
                    Ioc.Resolve<ICollisionDetector>("Detectors.Collision"),
                    (ICommand)order["onCollision"]
                );
            }
        ).Execute();
    }
}

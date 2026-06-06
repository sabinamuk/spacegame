using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public ShootCommand(IDictionary<string, object> order)
    {
        _order = order;
    }

    public void Execute()
    {
        var gameObjectId = (string)_order["gameObjectId"];
        var repository = (IGameObjectRepository)_order["repository"];
        var queue = (ICommandReceiver)_order["queue"];

        var ship = repository.Get(gameObjectId);
        var shipMovable = Ioc.Resolve<IMovable>("Adapters.IMovingObject", ship);

        var torpedoId = Ioc.Resolve<string>("Torpedo.NewId");
        var torpedo = new Dictionary<string, object>
        {
            ["position"] = shipMovable.Position,
            ["velocity"] = shipMovable.Velocity
        };

        repository.Set(torpedoId, torpedo);

        Ioc.Resolve<ICommand>("Actions.Start", new Dictionary<string, object>
        {
            ["operationType"] = "Move",
            ["queue"] = queue,
            ["gameObject"] = torpedo
        }).Execute();
    }
}

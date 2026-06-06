namespace SpaceBattle.Lib;

public class StopCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StopCommand(IDictionary<string, object> order)
    {
        _order = order;
    }

    public void Execute()
    {
        var opType = (string)_order["operationType"];
        var gameObject = (IDictionary<string, object>)_order["gameObject"];

        var injectable = (ICommandInjectable)gameObject["repeatable" + opType];
        injectable.Inject(new EmptyCommand());
    }
}

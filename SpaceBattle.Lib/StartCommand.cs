namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StartCommand(IDictionary<string, object> order)
    {
        _order = order;
    }

    public void Execute()
    {
        var opType = (string)_order["operationType"];
        var queue = (ICommandReceiver)_order["queue"];
        var gameObject = (IDictionary<string, object>)_order["gameObject"];

        var macro = (ICommand)Ioc.Resolve("Macro." + opType, new Dictionary<string, object>());

        var injectable = (CommandInjectableCommand)Ioc.Resolve(
            "Commands.CommandInjectable", new Dictionary<string, object>());
        injectable.Inject(macro);

        gameObject["repeatable" + opType] = injectable;

        new SendCommand(injectable, queue).Execute();
    }
}

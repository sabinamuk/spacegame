using Ioc = App.Ioc;

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

        var macro = Ioc.Resolve<ICommand>("Macro." + opType);

        var injectable = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        injectable.Inject(macro);

        gameObject["repeatable" + opType] = injectable;

        new SendCommand(injectable, queue).Execute();
    }
}

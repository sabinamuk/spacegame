namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Register("Actions.Stop", args => new StopCommand(args));
    }
}

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        new RegisterIoCDependencyMacroCommand().Execute();
        new RegisterDependencyCommandInjectableCommand().Execute();
        new RegisterIoCDependencySendCommand().Execute();
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Ioc.Register("Actions.Start", args => new StartCommand(args));
    }
}

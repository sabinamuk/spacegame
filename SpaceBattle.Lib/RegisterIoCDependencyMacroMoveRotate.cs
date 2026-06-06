using Ioc = App.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Macro.Move", (object[] _) =>
        {
            var strategy = new CreateMacroCommandStrategy("Move");
            return strategy.Resolve(new object[0]);
        }).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Macro.Rotate", (object[] _) =>
        {
            var strategy = new CreateMacroCommandStrategy("Rotate");
            return strategy.Resolve(new object[0]);
        }).Execute();
    }
}

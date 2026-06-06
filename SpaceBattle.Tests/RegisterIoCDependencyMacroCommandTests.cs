using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMacroCommandTests
{
    public RegisterIoCDependencyMacroCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void MacroCommand_Dependency_IsRegistered()
    {
        new RegisterIoCDependencyMacroCommand().Execute();

        var cmd1 = new Mock<ICommand>().Object;
        var cmd2 = new Mock<ICommand>().Object;

        var result = Ioc.Resolve<MacroCommand>("Commands.Macro",
            new List<ICommand> { cmd1, cmd2 });

        Assert.NotNull(result);
        Assert.IsType<MacroCommand>(result);
    }
}

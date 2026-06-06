using Ioc = App.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MacroMoveRotateTests
{
    public MacroMoveRotateTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void MacroMove_Throws_When_Specs_Missing()
    {
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.ThrowsAny<Exception>(() => Ioc.Resolve<ICommand>("Macro.Move"));
    }

    [Fact]
    public void Macro_Throws_When_Command_Not_Found()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Specs.Move",
            (object[] _) => new List<string> { "Commands.NotExists" }
        ).Execute();

        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.ThrowsAny<Exception>(() => Ioc.Resolve<ICommand>("Macro.Move"));
    }
}

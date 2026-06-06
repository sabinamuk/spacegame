using Moq;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class CreateMacroCommandStrategyTests
{
    public CreateMacroCommandStrategyTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void Resolve_Returns_MacroCommand()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Specs.Test",
            (object[] _) => new List<string> { "Commands.A", "Commands.B" }
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.A",
            (object[] _) => new Mock<ICommand>().Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.B",
            (object[] _) => new Mock<ICommand>().Object
        ).Execute();

        var strategy = new CreateMacroCommandStrategy("Test");
        var result = strategy.Resolve(new object[0]);

        Assert.IsType<MacroCommand>(result);
    }
}

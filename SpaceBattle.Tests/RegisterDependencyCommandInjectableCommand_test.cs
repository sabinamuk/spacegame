using App.Scopes;
using SpaceBattle.Lib;
using Ioc = App.Ioc;

namespace SpaceBattle.Tests;

public class RegisterDependencyCommandInjectableCommand_test
{
    public RegisterDependencyCommandInjectableCommand_test()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Resolve_ReturnsICommand()
    {
        new RegisterDependencyCommandInjectableCommand().Execute();
        var result = Ioc.Resolve<ICommand>("Commands.CommandInjectable");
        Assert.IsAssignableFrom<ICommand>(result);
    }

    [Fact]
    public void Resolve_ReturnsICommandInjectable()
    {
        new RegisterDependencyCommandInjectableCommand().Execute();
        var result = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        Assert.IsAssignableFrom<ICommandInjectable>(result);
    }

    [Fact]
    public void Resolve_ReturnsCommandInjectableCommand()
    {
        new RegisterDependencyCommandInjectableCommand().Execute();
        var result = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        Assert.IsAssignableFrom<CommandInjectableCommand>(result);
    }
}

using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterDependencyCommandInjectableCommand_test
{
    // Cuz we don't have <T> in Ioc.Resolve(...), gonna just check with Assert

    [Fact]
    public void Resolve_ReturnsICommand()
    {
        RegisterDependencyCommandInjectableCommand c = new RegisterDependencyCommandInjectableCommand();
        c.Execute();

        var result = Ioc.Resolve("Commands.CommandInjectable", new Dictionary<string, object>());
        Assert.IsAssignableFrom<ICommand>(result);
    }

    [Fact]
    public void Resolve_ReturnsICommandInjectable()
    {
        RegisterDependencyCommandInjectableCommand c = new RegisterDependencyCommandInjectableCommand();
        c.Execute();

        var result = Ioc.Resolve("Commands.CommandInjectable", new Dictionary<string, object>());
        Assert.IsAssignableFrom<ICommandInjectable>(result);
    }

    [Fact]
    public void Resolve_ReturnsCommandInjectableCommand()
    {
        RegisterDependencyCommandInjectableCommand c = new RegisterDependencyCommandInjectableCommand();
        c.Execute();

        var result = Ioc.Resolve("Commands.CommandInjectable", new Dictionary<string, object>());
        Assert.IsAssignableFrom<CommandInjectableCommand>(result);
    }
}


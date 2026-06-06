using Ioc = App.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyRotateCommandTests
{
    public RegisterIoCDependencyRotateCommandTests()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void Execute_RegisterRotateDependency()
    {
        var rotatingObject = new Mock<IRotatingObject>().Object;

        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IRotatingObject",
            (object[] _) => rotatingObject
        ).Execute();

        new RegisterIoCDependencyRotateCommand().Execute();

        var command = Ioc.Resolve<ICommand>("Commands.Rotate", new object());

        Assert.NotNull(command);
        Assert.IsType<RotateCommand>(command);
    }
}

using Moq;
using Xunit;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyRotateCommandTests
{
    [Fact]
    public void Dependency_Is_Resolved_After_Registration()
    {
        var rotatingObject =
            new Mock<IRotatingObject>().Object;

        Ioc.Register(
            "Adapters.IRotatingObject",
            args => rotatingObject);

        var register =
            new RegisterIoCDependencyRotateCommand();

        register.Execute();

        var command =
            Ioc.Resolve<ICommand>(
                "Commands.Rotate",
                new object());

        Assert.NotNull(command);

        Assert.IsType<RotateCommand>(command);
    }
}

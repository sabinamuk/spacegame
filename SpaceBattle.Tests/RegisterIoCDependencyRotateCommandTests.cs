using Moq;
using Xunit;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyRotateCommandTests
{
    [Fact]
    public void Execute_RegisterRotateDependency()
    {
        var rotatingObject =
            new Mock<IRotatingObject>().Object;

        Ioc.Register("Adapters.IRotatingObject",
            args => rotatingObject);

        new RegisterIoCDependencyRotateCommand()
            .Execute();

        var command =
            Ioc.Resolve(
                "Commands.Rotate",
                new Dictionary<string, object>
                {
                    { "obj", new object() }
                });

        Assert.NotNull(command);
        Assert.IsType<RotateCommand>(command);
    }
}

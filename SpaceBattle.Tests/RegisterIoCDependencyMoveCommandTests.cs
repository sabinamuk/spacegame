using SpaceBattle.Lib;
using Moq;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void MoveDependency_IsRegistered()
    {
        Ioc.Register("Adapters.IMovingObject", _ => new TestAdapter());

        new RegisterIoCDependencyMoveCommand().Execute();

        var obj = new Mock<IMovingObject>().Object;

        var cmd = Ioc.Resolve(
            "Commands.Move",
            new Dictionary<string, object> { { "obj", obj } }
        );

        Assert.NotNull(cmd);
    }
}
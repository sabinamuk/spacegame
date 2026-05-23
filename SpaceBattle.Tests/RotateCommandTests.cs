using SpaceBattle.Lib;
using Moq;
using System.Reflection.Metadata;
using Xunit;


namespace SpaceBattle.Tests;

public class RotateCommandTests
{
    [Fact]
    public void Execute_RotateObject()
    {
        var mock = new Mock<IRotatingObject>();

        mock.SetupProperty(o => o.Angle, new Angle(1, 8));
        mock.SetupGet(o => o.AngularVelocity)
            .Returns(new Angle(1, 8));

        var cmd = new RotateCommand(mock.Object);

        cmd.Execute();

        Assert.Equal(new Angle(2, 8), mock.Object.Angle);
    }

    [Fact]
    public void Execute_Throws_When_Angle_Cannot_Be_Read()
    {
        var mock = new Mock<IRotatingObject>();

        mock.SetupGet(o => o.Angle)
            .Throws(new Exception());

        var cmd = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void Execute_Throws_When_AngularVelocity_Cannot_Be_Read()
    {
        var mock = new Mock<IRotatingObject>();

        mock.SetupProperty(o => o.Angle, new Angle(1, 8));

        mock.SetupGet(o => o.AngularVelocity)
            .Throws(new Exception());

        var cmd = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void Execute_Throws_When_Angle_Cannot_Be_Set()
    {
        var mock = new Mock<IRotatingObject>();

        mock.SetupGet(o => o.Angle)
            .Returns(new Angle(1, 8));

        mock.SetupGet(o => o.AngularVelocity)
            .Returns(new Angle(1, 8));

        mock.SetupSet(o => o.Angle = It.IsAny<Angle>())
            .Throws(new Exception());

        var cmd = new RotateCommand(mock.Object);

        Assert.Throws<Exception>(() => cmd.Execute());
    }
}

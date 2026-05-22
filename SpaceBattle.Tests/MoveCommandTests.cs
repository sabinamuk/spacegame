using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MoveCommandTests
{
    [Fact]
    public void Move_ValidObject_MovesCorrectly()
    {
        var obj = new TestObject(
            new Vector(12, 5),
            new Vector(-4, 1)
        );

        var cmd = new MoveCommand(obj, obj);

        cmd.Execute();

        Assert.Equal(new Vector(8, 6), obj.Position);
    }

    [Fact]
    public void Move_NoPosition_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var cmd = new MoveCommand(null, new TestObject(new Vector(1, 1), new Vector(1, 1)));
            cmd.Execute();
        });
    }

    [Fact]
    public void Move_NoVelocity_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var cmd = new MoveCommand(new TestObject(new Vector(1, 1), new Vector(1, 1)), null);
            cmd.Execute();
        });
    }

    [Fact]
    public void Move_PositionNotChangeable_Throws()
    {
        var obj = new TestObject(new Vector(1, 1), new Vector(1, 1));

        var cmd = new MoveCommand(obj, obj);

        obj.Position = null;

        Assert.Throws<NullReferenceException>(() => cmd.Execute());
    }
}

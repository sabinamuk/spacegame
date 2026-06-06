using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MoveCommandTests
{

    private class MovableFake : IMovable
    {
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }

        public MovableFake(Vector position, Vector velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }

    [Fact]
    public void Move_ValidObject_MovesCorrectly()
    {
        var obj = new MovableFake(
            new Vector(12, 5),
            new Vector(-4, 1)
        );

        var cmd = new MoveCommand(obj);

        cmd.Execute();

        Assert.Equal(new Vector(8, 6), obj.Position);
    }

    [Fact]
    public void Move_NoPosition_Throws()
    {
        var obj = new MovableFake(null, new Vector(1, 1));

        var cmd = new MoveCommand(obj);

        Assert.Throws<ArgumentNullException>(() => cmd.Execute());
    }

    [Fact]
    public void Move_NoVelocity_Throws()
    {
        var obj = new MovableFake(new Vector(1, 1), null);

        var cmd = new MoveCommand(obj);

        Assert.Throws<ArgumentNullException>(() => cmd.Execute());
    }

    [Fact]
    public void Move_PositionBecomesNull_Throws()
    {
        var obj = new MovableFake(new Vector(1, 1), new Vector(1, 1));

        var cmd = new MoveCommand(obj);

        obj.Position = null;

        Assert.Throws<ArgumentNullException>(() => cmd.Execute());
    }
}

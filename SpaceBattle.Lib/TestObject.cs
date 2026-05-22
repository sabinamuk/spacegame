using SpaceBattle.Lib;

public class TestObject : IHasPosition, IHasVelocity
{
    public Vector Position { get; set; }
    public Vector Velocity { get; }

    public TestObject(Vector position, Vector velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}

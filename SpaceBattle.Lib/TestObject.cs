namespace SpaceBattle.Lib;

public class TestObject : IMovable
{
    public Vector Position { get; set; }
    public Vector Velocity { get; set; }

    public TestObject(Vector position, Vector velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}

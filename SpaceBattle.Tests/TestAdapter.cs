using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class TestAdapter : IMovable
{
    public Vector Position { get; set; } = new Vector(0, 0);
    public Vector Velocity { get; } = new Vector(1, 1);
}

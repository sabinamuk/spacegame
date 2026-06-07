namespace SpaceBattle.Lib;

public class CollisionEntry
{
    public string ShapeA { get; }
    public string ShapeB { get; }
    public int RelX { get; }
    public int RelY { get; }

    public CollisionEntry(string shapeA, string shapeB, int relX, int relY)
    {
        ShapeA = shapeA;
        ShapeB = shapeB;
        RelX = relX;
        RelY = relY;
    }

    public override bool Equals(object? obj) =>
        obj is CollisionEntry e && e.ShapeA == ShapeA && e.ShapeB == ShapeB && e.RelX == RelX && e.RelY == RelY;

    public override int GetHashCode() => HashCode.Combine(ShapeA, ShapeB, RelX, RelY);
}

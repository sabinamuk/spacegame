namespace SpaceBattle.Lib;

public class CollisionEntry
{
    public string ShapeA { get; }
    public string ShapeB { get; }
    public int RelX { get; }
    public int RelY { get; }
    public int RelVX { get; }
    public int RelVY { get; }

    public CollisionEntry(string shapeA, string shapeB, int relX, int relY, int relVX, int relVY)
    {
        ShapeA = shapeA;
        ShapeB = shapeB;
        RelX = relX;
        RelY = relY;
        RelVX = relVX;
        RelVY = relVY;
    }

    public override bool Equals(object? obj) =>
        obj is CollisionEntry e
        && e.ShapeA == ShapeA && e.ShapeB == ShapeB
        && e.RelX == RelX && e.RelY == RelY
        && e.RelVX == RelVX && e.RelVY == RelVY;

    public override int GetHashCode() =>
        HashCode.Combine(ShapeA, ShapeB, RelX, RelY, RelVX, RelVY);
}

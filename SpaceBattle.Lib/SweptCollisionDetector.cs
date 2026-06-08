namespace SpaceBattle.Lib;

public class SweptCollisionDetector : ICollisionDetector
{
    private readonly IShapeProvider _shapes;

    public SweptCollisionDetector(IShapeProvider shapes)
    {
        _shapes = shapes;
    }

    public bool Collides(ICollidable a, ICollidable b)
    {
        var cellsA = _shapes.GetShape(a.GetShapeId()).Cells;
        var cellsB = _shapes.GetShape(b.GetShapeId()).Cells;

        float dvX = a.GetVelocity()[0] - b.GetVelocity()[0];
        float dvY = a.GetVelocity()[1] - b.GetVelocity()[1];

        foreach (var cellA in cellsA)
        {
            float sx = a.GetPosition()[0] + cellA.X;
            float sy = a.GetPosition()[1] + cellA.Y;

            foreach (var cellB in cellsB)
            {
                int cx = b.GetPosition()[0] + cellB.X;
                int cy = b.GetPosition()[1] + cellB.Y;

                if (SegmentHitsCell(sx, sy, sx + dvX, sy + dvY, cx, cy))
                    return true;
            }
        }

        return false;
    }

    private static bool SegmentHitsCell(float sx, float sy, float ex, float ey, int cx, int cy)
    {
        var (tEnterX, tExitX) = SlabInterval(sx, ex - sx, cx, cx + 1);
        var (tEnterY, tExitY) = SlabInterval(sy, ey - sy, cy, cy + 1);

        float tEnter = Math.Max(tEnterX, tEnterY);
        float tExit = Math.Min(tExitX, tExitY);

        return tEnter <= tExit && tExit >= 0f && tEnter <= 1f;
    }

    private static (float Enter, float Exit) SlabInterval(float s, float d, float min, float max)
    {
        if (MathF.Abs(d) < 1e-6f)
            return s >= min && s < max
                ? (float.NegativeInfinity, float.PositiveInfinity)
                : (float.PositiveInfinity, float.NegativeInfinity);

        float t1 = (min - s) / d;
        float t2 = (max - s) / d;
        return d < 0 ? (t2, t1) : (t1, t2);
    }
}

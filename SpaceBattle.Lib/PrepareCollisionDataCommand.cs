namespace SpaceBattle.Lib;

public class PrepareCollisionDataCommand : ICommand
{
    private readonly IShapeFootprint _shapeA;
    private readonly IShapeFootprint _shapeB;
    private readonly ICollisionMap _map;
    private readonly int _maxVelocity;

    public PrepareCollisionDataCommand(
        IShapeFootprint shapeA,
        IShapeFootprint shapeB,
        ICollisionMap map,
        int maxVelocity = 0)
    {
        _shapeA = shapeA;
        _shapeB = shapeB;
        _map = map;
        _maxVelocity = maxVelocity;
    }

    public void Execute()
    {
        foreach (var cellA in _shapeA.Cells)
            foreach (var cellB in _shapeB.Cells)
            {
                int colDx = cellA.X - cellB.X;
                int colDy = cellA.Y - cellB.Y;

                for (int dvx = -_maxVelocity; dvx <= _maxVelocity; dvx++)
                    for (int dvy = -_maxVelocity; dvy <= _maxVelocity; dvy++)
                    {
                        int xFrom = Math.Min(colDx, colDx - dvx);
                        int xTo = Math.Max(colDx, colDx - dvx);
                        int yFrom = Math.Min(colDy, colDy - dvy);
                        int yTo = Math.Max(colDy, colDy - dvy);

                        for (int x = xFrom; x <= xTo; x++)
                            for (int y = yFrom; y <= yTo; y++)
                                if (SweptCollisionDetector.SegmentHitsCell(x, y, x + dvx, y + dvy, colDx, colDy))
                                    _map.Add(_shapeA.ShapeId, _shapeB.ShapeId, x, y, dvx, dvy);
                    }
            }
    }
}

namespace SpaceBattle.Lib;

public class PrepareCollisionDataCommand : ICommand
{
    private readonly IShapeFootprint _shapeA;
    private readonly IShapeFootprint _shapeB;
    private readonly ICollisionMap _map;

    public PrepareCollisionDataCommand(IShapeFootprint shapeA, IShapeFootprint shapeB, ICollisionMap map)
    {
        _shapeA = shapeA;
        _shapeB = shapeB;
        _map = map;
    }

    public void Execute()
    {
        foreach (var cellA in _shapeA.Cells)
            foreach (var cellB in _shapeB.Cells)
                _map.Add(_shapeA.ShapeId, _shapeB.ShapeId, cellA.X - cellB.X, cellA.Y - cellB.Y);
    }
}

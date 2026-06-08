namespace SpaceBattle.Lib;

public class ShapeProvider : IShapeProvider
{
    private readonly Dictionary<string, IShapeFootprint> _shapes = new();

    public IShapeFootprint GetShape(string shapeId) => _shapes[shapeId];

    public void Register(IShapeFootprint shape) => _shapes[shape.ShapeId] = shape;
}

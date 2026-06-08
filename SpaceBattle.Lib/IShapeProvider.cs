namespace SpaceBattle.Lib;

public interface IShapeProvider
{
    IShapeFootprint GetShape(string shapeId);
    void Register(IShapeFootprint shape);
}

namespace SpaceBattle.Lib;

public interface IShapeFootprint
{
    string ShapeId { get; }
    IReadOnlyList<(int X, int Y)> Cells { get; }
}

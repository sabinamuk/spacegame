namespace SpaceBattle.Lib;

public class CollisionTableGenerator
{
    private readonly IReadOnlyList<ObjectShape> _shapes;
    private readonly int _searchRadius;

    public CollisionTableGenerator(IReadOnlyList<ObjectShape> shapes, int searchRadius = 10)
    {
        _shapes = shapes;
        _searchRadius = searchRadius;
    }

    public Dictionary<(string, string, int, int), bool> Generate()
    {
        var table = new Dictionary<(string, string, int, int), bool>();

        foreach (var a in _shapes)
        foreach (var b in _shapes)
        for (var dx = -_searchRadius; dx <= _searchRadius; dx++)
        for (var dy = -_searchRadius; dy <= _searchRadius; dy++)
        {
            var distance = Math.Sqrt(dx * dx + dy * dy);
            table[(a.Type, b.Type, dx, dy)] = distance < a.Radius + b.Radius;
        }

        return table;
    }
}

namespace SpaceBattle.Lib;

public class LookupCollisionDetector : ICollisionDetector
{
    private readonly IReadOnlyDictionary<(string, string, int, int), bool> _table;

    public LookupCollisionDetector(IReadOnlyDictionary<(string, string, int, int), bool> table)
    {
        _table = table;
    }

    public bool Detect(IDictionary<string, object> a, IDictionary<string, object> b)
    {
        var typeA = (string)a["type"];
        var typeB = (string)b["type"];
        var posA = (Vector)a["position"];
        var posB = (Vector)b["position"];
        var dx = posA[0] - posB[0];
        var dy = posA[1] - posB[1];

        if (_table.TryGetValue((typeA, typeB, dx, dy), out var result))
            return result;

        if (_table.TryGetValue((typeB, typeA, -dx, -dy), out result))
            return result;

        return false;
    }
}

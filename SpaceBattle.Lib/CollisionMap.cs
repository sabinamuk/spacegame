namespace SpaceBattle.Lib;

public class CollisionMap : ICollisionMap
{
    // dx → dy → dvx → HashSet<dvy>
    private readonly Dictionary<(string, string), Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>> _data = new();

    public bool Contains(string shapeA, string shapeB, int dx, int dy, int dvx, int dvy)
        => ContainsDirected(shapeA, shapeB, dx, dy, dvx, dvy)
        || ContainsDirected(shapeB, shapeA, -dx, -dy, -dvx, -dvy);

    private bool ContainsDirected(string a, string b, int dx, int dy, int dvx, int dvy)
        => _data.TryGetValue((a, b), out var t1)
        && t1.TryGetValue(dx, out var t2)
        && t2.TryGetValue(dy, out var t3)
        && t3.TryGetValue(dvx, out var t4)
        && t4.Contains(dvy);

    public void Add(string shapeA, string shapeB, int dx, int dy, int dvx, int dvy)
    {
        var key = (shapeA, shapeB);
        if (!_data.TryGetValue(key, out var t1)) _data[key] = t1 = new();
        if (!t1.TryGetValue(dx, out var t2)) t1[dx] = t2 = new();
        if (!t2.TryGetValue(dy, out var t3)) t2[dy] = t3 = new();
        if (!t3.TryGetValue(dvx, out var t4)) t3[dvx] = t4 = new();
        t4.Add(dvy);
    }

    public IReadOnlyList<CollisionEntry> GetEntries()
    {
        var result = new List<CollisionEntry>();
        foreach (var ((shapeA, shapeB), t1) in _data)
            foreach (var (dx, t2) in t1)
                foreach (var (dy, t3) in t2)
                    foreach (var (dvx, t4) in t3)
                        foreach (var dvy in t4)
                            result.Add(new CollisionEntry(shapeA, shapeB, dx, dy, dvx, dvy));
        return result;
    }
}

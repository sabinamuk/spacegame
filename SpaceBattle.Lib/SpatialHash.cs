namespace SpaceBattle.Lib;

public class SpatialHash : ISpatialHash
{
    private readonly int _cellSize;
    private readonly Dictionary<(int, int), List<(string Id, ICollidable Collidable)>> _cells = new();
    private readonly Dictionary<string, (int, int)> _idToCell = new();

    public SpatialHash(int cellSize = 10)
    {
        _cellSize = cellSize;
    }

    private (int, int) GetCell(Vector pos) =>
        ((int)Math.Floor((double)pos[0] / _cellSize),
         (int)Math.Floor((double)pos[1] / _cellSize));

    public void Add(string id, ICollidable obj)
    {
        var cell = GetCell(obj.GetPosition());
        if (!_cells.ContainsKey(cell))
            _cells[cell] = new List<(string, ICollidable)>();
        _cells[cell].Add((id, obj));
        _idToCell[id] = cell;
    }

    public void Remove(string id)
    {
        if (!_idToCell.TryGetValue(id, out var cell))
            return;
        _cells[cell].RemoveAll(e => e.Id == id);
        _idToCell.Remove(id);
    }

    public IEnumerable<(string Id, ICollidable Collidable)> GetNearby(Vector position)
    {
        var (cx, cy) = GetCell(position);
        for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (_cells.TryGetValue((cx + dx, cy + dy), out var list))
                    foreach (var entry in list)
                        yield return entry;
            }
    }
}

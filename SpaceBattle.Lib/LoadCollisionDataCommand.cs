using System.Text.Json;

namespace SpaceBattle.Lib;

public class LoadCollisionDataCommand : ICommand
{
    private readonly ICollisionMap _map;
    private readonly string _path;

    public LoadCollisionDataCommand(ICollisionMap map, string path)
    {
        _map = map;
        _path = path;
    }

    public void Execute()
    {
        var entries = JsonSerializer.Deserialize<List<CollisionEntry>>(File.ReadAllText(_path),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        foreach (var e in entries)
            _map.Add(e.ShapeA, e.ShapeB, e.RelX, e.RelY);
    }
}

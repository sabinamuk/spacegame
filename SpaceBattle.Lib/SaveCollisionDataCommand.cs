using System.Text.Json;

namespace SpaceBattle.Lib;

public class SaveCollisionDataCommand : ICommand
{
    private readonly ICollisionMap _map;
    private readonly string _path;

    public SaveCollisionDataCommand(ICollisionMap map, string path)
    {
        _map = map;
        _path = path;
    }

    public void Execute()
    {
        var json = JsonSerializer.Serialize(_map.GetEntries(), new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }
}

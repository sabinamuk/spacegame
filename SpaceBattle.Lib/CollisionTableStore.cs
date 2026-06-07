using System.Text.Json;

namespace SpaceBattle.Lib;

public static class CollisionTableStore
{
    public static void Save(Dictionary<(string, string, int, int), bool> table, string path)
    {
        var dict = table.ToDictionary(
            kv => $"{kv.Key.Item1}|{kv.Key.Item2}|{kv.Key.Item3}|{kv.Key.Item4}",
            kv => kv.Value
        );
        File.WriteAllText(path, JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true }));
    }

    public static Dictionary<(string, string, int, int), bool> Load(string path)
    {
        var raw = JsonSerializer.Deserialize<Dictionary<string, bool>>(File.ReadAllText(path))!;
        return raw.ToDictionary(
            kv =>
            {
                var p = kv.Key.Split('|');
                return (p[0], p[1], int.Parse(p[2]), int.Parse(p[3]));
            },
            kv => kv.Value
        );
    }
}

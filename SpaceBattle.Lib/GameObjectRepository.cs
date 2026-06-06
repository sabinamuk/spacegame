namespace SpaceBattle.Lib;

public class GameObjectRepository
    : IGameObjectRepository
{
    private readonly Dictionary<string, object>
        objects = new();

    public void Add(
        string id,
        object gameObject)
    {
        objects[id] = gameObject;
    }

    public object Get(
        string id)
    {
        return objects[id];
    }

    public bool Contains(
        string id)
    {
        return objects.ContainsKey(id);
    }

    public int Count()
    {
        return objects.Count;
    }
}

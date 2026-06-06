namespace SpaceBattle.Lib;

public class GameObjectRepository : IGameObjectRepository
{
    private readonly Dictionary<string, IDictionary<string, object>> _objects = new();

    public IDictionary<string, object> Get(string id) => _objects[id];

    public void Set(string id, IDictionary<string, object> gameObject) => _objects[id] = gameObject;

    public void Remove(string id) => _objects.Remove(id);

    public int Count => _objects.Count;
}

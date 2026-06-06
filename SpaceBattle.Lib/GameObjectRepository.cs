namespace SpaceBattle.Lib;

public class GameObjectRepository : IGameObjectRepository
{
    private readonly Dictionary<string, IDictionary<string, object>> _storage = new();

    public IDictionary<string, object> Get(string id) => _storage[id];

    public void Set(string id, IDictionary<string, object> gameObject) => _storage[id] = gameObject;

    public void Remove(string id) => _storage.Remove(id);

    public IEnumerable<string> GetIds() => _storage.Keys;
}

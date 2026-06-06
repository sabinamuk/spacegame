namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    IDictionary<string, object> Get(string id);
    void Set(string id, IDictionary<string, object> gameObject);
    void Remove(string id);
}

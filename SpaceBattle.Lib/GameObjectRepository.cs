namespace SpaceBattle.Lib;

public class GameObjectRepository
    : IGameObjectRepository
{
    private readonly Dictionary<string, IGameObject>
        objects = new();

    public void Add(
        string id,
        IGameObject gameObject)
    {
        objects[id] = gameObject;
    }

    public IGameObject Get(
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

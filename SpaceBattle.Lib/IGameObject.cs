namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    void Add(
        string id,
        IGameObject gameObject);

    IGameObject Get(
        string id);

    bool Contains(
        string id);

    int Count();
}

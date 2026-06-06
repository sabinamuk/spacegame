namespace SpaceBattle.Lib;

public interface IGameObjectRepository
{
    void Add(
        string id,
        object gameObject);

    object Get(
        string id);

    bool Contains(
        string id);

    int Count();
}

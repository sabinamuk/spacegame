namespace SpaceBattle.Lib;

public interface ISpatialHash
{
    void Add(string id, ICollidable obj);
    void Remove(string id);
    IEnumerable<(string Id, ICollidable Collidable)> GetNearby(Vector position);
}

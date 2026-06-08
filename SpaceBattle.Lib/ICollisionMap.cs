namespace SpaceBattle.Lib;

public interface ICollisionMap
{
    bool Contains(string shapeA, string shapeB, int relX, int relY, int relVX, int relVY);
    void Add(string shapeA, string shapeB, int relX, int relY, int relVX, int relVY);
    IReadOnlyList<CollisionEntry> GetEntries();
}

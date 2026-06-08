namespace SpaceBattle.Lib;

public interface ICollidableRepository
{
    IEnumerable<(string Id, ICollidable Collidable)> GetAll();
}

namespace SpaceBattle.Lib;

public class CheckCollisionCommand : ICommand
{
    private readonly string _selfId;
    private readonly ICollidable _self;
    private readonly ISpatialHash _spatialHash;
    private readonly ICollisionDetector _detector;
    private readonly ICommand _onCollision;

    public CheckCollisionCommand(
        string selfId,
        ICollidable self,
        ISpatialHash spatialHash,
        ICollisionDetector detector,
        ICommand onCollision)
    {
        _selfId = selfId;
        _self = self;
        _spatialHash = spatialHash;
        _detector = detector;
        _onCollision = onCollision;
    }

    public void Execute()
    {
        foreach (var (id, other) in _spatialHash.GetNearby(_self.GetPosition()))
        {
            if (id == _selfId)
                continue;

            if (_detector.Collides(_self, other))
            {
                _onCollision.Execute();
                return;
            }
        }
    }
}

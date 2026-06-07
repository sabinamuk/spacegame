namespace SpaceBattle.Lib;

public class CheckCollisionCommand : ICommand
{
    private readonly string _selfId;
    private readonly ICollidable _self;
    private readonly ICollidableRepository _repository;
    private readonly ICollisionDetector _detector;
    private readonly ICommand _onCollision;

    public CheckCollisionCommand(
        string selfId,
        ICollidable self,
        ICollidableRepository repository,
        ICollisionDetector detector,
        ICommand onCollision)
    {
        _selfId = selfId;
        _self = self;
        _repository = repository;
        _detector = detector;
        _onCollision = onCollision;
    }

    public void Execute()
    {
        foreach (var (id, other) in _repository.GetAll())
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

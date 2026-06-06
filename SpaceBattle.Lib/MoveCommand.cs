namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IMovable _movable;

    public MoveCommand(IMovable movable)
    {
        _movable = movable ?? throw new ArgumentNullException(nameof(movable));
    }

    public void Execute()
    {
        if (_movable.Position is null)
            throw new ArgumentNullException(nameof(_movable.Position));

        if (_movable.Velocity is null)
            throw new ArgumentNullException(nameof(_movable.Velocity));

        _movable.Position = _movable.Position + _movable.Velocity;
    }
}

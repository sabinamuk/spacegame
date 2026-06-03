namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private readonly IHasPosition _position;
    private readonly IHasVelocity _velocity;

    public MoveCommand(IHasPosition position, IHasVelocity velocity)
    {
        _position = position;
        _velocity = velocity;
    }

    public void Execute()
    {
        if (_position == null)
            throw new ArgumentNullException(nameof(_position));

        if (_velocity == null)
            throw new ArgumentNullException(nameof(_velocity));

        _position.Position = _position.Position + _velocity.Velocity;
    }
}

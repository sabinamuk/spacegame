namespace SpaceBattle.Lib;

public class Game : ICommandReceiver
{
    private readonly Queue<ICommand> _commands = new();

    public void Receive(ICommand command) => _commands.Enqueue(command);

    public void Update()
    {
        if (_commands.Count > 0)
            _commands.Dequeue().Execute();
    }
}

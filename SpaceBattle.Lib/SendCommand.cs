
namespace SpaceBattle.Lib
{
    public class SendCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly IMessageReceiver _receiver;

        public SendCommand(ICommand command, IMessageReceiver receiver)
        {
            _command = command;
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Receive(_command);
        }
    }
}


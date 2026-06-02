using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoDependencySendCommand_test
{
    [Fact]
    public void Execute_RegistersCommandsSend_ResolvesAndCallsReceive()
    {
        // arrange
        var commandMock = new Mock<ICommand>();
        var receiverMock = new Mock<IMessageReceiver>();


        // act
        new RegisterIoCDependencySendCommand().Execute();

        var args = new Dictionary<string, object>
        {
            ["command"] = commandMock.Object,
            ["receiver"] = receiverMock.Object
        };

        var sendCommand = (ICommand)Ioc.Resolve("Commands.Send", args);
        sendCommand.Execute();

        // assert

        receiverMock.Verify(r => r.Receive(commandMock.Object), Times.Once);
    }
}



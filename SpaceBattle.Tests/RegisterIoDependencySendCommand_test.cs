using Ioc = App.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoDependencySendCommand_test
{
    public RegisterIoDependencySendCommand_test()
    {
        new App.Scopes.InitCommand().Execute();
        new App.Scopes.ClearCurrentScopeCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    [Fact]
    public void Execute_RegistersCommandsSend_ResolvesAndCallsReceive()
    {
        var commandMock = new Mock<ICommand>();
        var receiverMock = new Mock<ICommandReceiver>();

        new RegisterIoCDependencySendCommand().Execute();

        var sendCommand = Ioc.Resolve<ICommand>("Commands.Send",
            commandMock.Object, receiverMock.Object);
        sendCommand.Execute();

        receiverMock.Verify(r => r.Receive(commandMock.Object), Times.Once);
    }
}

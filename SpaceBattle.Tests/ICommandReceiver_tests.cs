using Moq;
using SpaceBattle.Lib;
using Xunit.Sdk;
namespace SpaceBattle.Tests;
public class IMessageReceiver_tests
{
    [Fact]
    public void IMessageReceiver_SendCommandExecuteCallsReceiveWithCorrectCommand()
    {
        //Arrange
        var mockCommand = new Mock<ICommand>();
        var mockReceiver = new Mock<IMessageReceiver>();
        var sendCommand = new SendCommand(mockCommand.Object, mockReceiver.Object);

        //Act
        sendCommand.Execute();
        //Assert
        mockReceiver.Verify(r => r.Receive(mockCommand.Object), Times.Once); //Проверяем историю вызовов, что метод Receive был вызван один раз, а в аргументе правильная команда
    }
    class Faulty_Receiver : IMessageReceiver
    {
        public void Receive(ICommand command)
        {
            throw new InvalidOperationException("Cannot receive command");
            //Не может принять команду
        }
    }
    [Fact]
    public void IMessageReceiver_ThrowsExceptionWhenReceiverFails()
    {
        //Arrange
        var bad_receiver = new Faulty_Receiver();
        var mockCommand = new Mock<ICommand>();
        var sendCommand = new SendCommand(mockCommand.Object, bad_receiver);
        bool error_returned = false;
        //Act
        try
        {
            sendCommand.Execute();
        }
        catch (InvalidOperationException)
        {
            error_returned = true;
        }
        //Assert
        Assert.True(error_returned, "Ожидали получить InvalidOperationException при вызове Execute у SendCommand с IMessageReceiver, всегда возвращающим исключение");
    }
}
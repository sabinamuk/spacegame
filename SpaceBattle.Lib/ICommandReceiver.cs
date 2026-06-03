
namespace SpaceBattle.Lib;

public interface ICommandReceiver //Интерфейс для получения команды, которая будет выполняться в другом месте(Может реализовывать класс очереди для команд, например)
{
    void Receive(ICommand command);
}


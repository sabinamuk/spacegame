namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObjectRepository : ICommand
{
    public void Execute()
    {
        var repository = new GameObjectRepository();
        Ioc.Register("Game.Objects.Repository", _ => (object)repository);
    }
}

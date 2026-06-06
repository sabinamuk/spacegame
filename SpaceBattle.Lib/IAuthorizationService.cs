namespace SpaceBattle.Lib;

public interface IAuthorizationService
{
    bool IsAuthorized(string gameId, string objectId, string playerId);
}

using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CheckAuthorizationCommandTests
{
    private readonly Mock<IAuthorizationService> _authService = new();

    [Fact]
    public void Execute_AuthorizedPlayer_DoesNotThrow()
    {
        _authService.Setup(s => s.IsAuthorized("game1", "ship1", "player1")).Returns(true);

        var cmd = new CheckAuthorizationCommand(_authService.Object, "game1", "ship1", "player1");

        var ex = Record.Exception(() => cmd.Execute());
        Assert.Null(ex);
    }

    [Fact]
    public void Execute_UnauthorizedPlayer_Throws()
    {
        _authService.Setup(s => s.IsAuthorized("game1", "ship1", "player1")).Returns(false);

        var cmd = new CheckAuthorizationCommand(_authService.Object, "game1", "ship1", "player1");

        Assert.Throws<UnauthorizedAccessException>(() => cmd.Execute());
    }

    [Fact]
    public void Execute_ChecksCorrectGameObjectAndPlayer()
    {
        _authService.Setup(s => s.IsAuthorized("game1", "ship1", "player1")).Returns(true);

        new CheckAuthorizationCommand(_authService.Object, "game1", "ship1", "player1").Execute();

        _authService.Verify(s => s.IsAuthorized("game1", "ship1", "player1"), Times.Once);
    }
}

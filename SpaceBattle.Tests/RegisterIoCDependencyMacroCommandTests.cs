using Xunit;
using Moq;
using System.Collections.Generic;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyMacroCommandTests
{
    [Fact]
    public void MacroCommand_Dependency_IsRegistered()
    {
        new RegisterIoCDependencyMacroCommand()
            .Execute();

        var cmd1 = new Mock<ICommand>().Object;
        var cmd2 = new Mock<ICommand>().Object;

        var result =
            Ioc.Resolve(
                "Commands.Macro",
                new Dictionary<string, object>
                {
                    {
                        "commands",
                        new List<ICommand>
                        {
                            cmd1,
                            cmd2
                        }
                    }
                });

        Assert.NotNull(result);
        Assert.IsType<MacroCommand>(result);
    }
}

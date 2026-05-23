using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void Resolve_Returns_MacroCommand()
    {
        Ioc.Register(
            "Specs.Test",
            _ => new List<string>
            {
                "Commands.A",
                "Commands.B"
            });

        Ioc.Register(
            "Commands.A",
            _ => new Mock<ICommand>().Object);

        Ioc.Register(
            "Commands.B",
            _ => new Mock<ICommand>().Object);

        var strategy =
            new CreateMacroCommandStrategy("Test");

        var result =
            strategy.Resolve(new object[0]);

        Assert.IsType<MacroCommand>(result);
    }
}

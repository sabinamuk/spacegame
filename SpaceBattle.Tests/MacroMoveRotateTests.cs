using Xunit;
using Moq;
using System.Collections.Generic;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MacroMoveRotateTests
{

    [Fact]
    public void MacroMove_Throws_When_Specs_Missing()
    {
        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.ThrowsAny<Exception>(() =>
            Ioc.Resolve(
                "Macro.Move",
                new Dictionary<string, object>()));
    }

    [Fact]
    public void Macro_Throws_When_Command_Not_Found()
    {
        Ioc.Register("Specs.Move", _ =>
            new List<string> { "Commands.NotExists" });

        new RegisterIoCDependencyMacroMoveRotate().Execute();

        Assert.Throws<KeyNotFoundException>(() =>
            Ioc.Resolve(
                "Macro.Move",
                new Dictionary<string, object>()));
    }
}

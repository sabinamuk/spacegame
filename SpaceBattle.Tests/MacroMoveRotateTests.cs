using Xunit;
using Moq;
using System.Collections.Generic;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class MacroMoveRotateTests
{

    [Fact]
    public void MacroRotate_Executes_All_Commands()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();

        Ioc.Register("Commands.A", _ => cmd1.Object);
        Ioc.Register("Commands.B", _ => cmd2.Object);

        Ioc.Register("Specs.Rotate", _ =>
            new List<string>
            {
                "Commands.A",
                "Commands.B"
            });

        new RegisterIoCDependencyMacroMoveRotate().Execute();

        var macro = (ICommand)Ioc.Resolve(
            "Macro.Rotate",
            new Dictionary<string, object>());

        macro.Execute();

        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
    }

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

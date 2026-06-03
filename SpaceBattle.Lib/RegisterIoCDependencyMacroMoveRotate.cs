using System;
using System.Collections.Generic;
using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Register("Macro.Move", _ =>
        {
            var strategy = new CreateMacroCommandStrategy("Move");
            return strategy.Resolve(new object[0]);
        });

        Ioc.Register("Macro.Rotate", _ =>
        {
            var strategy = new CreateMacroCommandStrategy("Rotate");
            return strategy.Resolve(new object[0]);
        });
    }
}

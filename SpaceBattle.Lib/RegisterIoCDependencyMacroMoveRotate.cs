using System;
using System.Collections.Generic;
using SpaceBattle.Lib;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        // Macro.Move
        Ioc.Register("Macro.Move", args =>
        {
            var strategy = new CreateMacroCommandStrategy("Move");
            return strategy.Resolve(args);
        });

        // Macro.Rotate
        Ioc.Register("Macro.Rotate", args =>
        {
            var strategy = new CreateMacroCommandStrategy("Rotate");
            return strategy.Resolve(args);
        });
    }
}

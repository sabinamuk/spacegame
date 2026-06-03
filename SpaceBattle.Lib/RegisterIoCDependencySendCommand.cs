using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencySendCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Commands.Send", args => new SendCommand((ICommand)args["command"], (ICommandReceiver)args["receiver"]));
        }
    }
}
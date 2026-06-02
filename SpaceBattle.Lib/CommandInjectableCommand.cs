using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Lib
{
    public class CommandInjectableCommand : ICommand, ICommandInjectable
    {
        private ICommand _injected_command;
        public void Inject(ICommand command)
        {
            _injected_command = command;
        }

        public void Execute()
        {
            if (_injected_command != null)
            {
                _injected_command.Execute();
            }
            else
            {
                throw new Exception("No injected command found; Inject command first before Execute().");
            }
        }
    }
}

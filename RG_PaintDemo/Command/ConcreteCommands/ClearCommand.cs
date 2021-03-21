using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RG_PaintDemo.ConcreteCommands
{
    public class ClearCommand : ICommand
    {
        public ClearCommand()
        {

        }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}

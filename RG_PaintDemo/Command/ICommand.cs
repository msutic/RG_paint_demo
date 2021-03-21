using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RG_PaintDemo
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}


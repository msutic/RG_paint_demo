using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace RG_PaintDemo
{
    public class Command
    {
        public string Action { get; set; }
        public int Index { get; set; }
        public object Obj { get; set; }

        public Command()
        {

        }

        public Command(string action, int index, object obj)
        {
            Action = action;
            Index = index;
            Obj = obj;
        }
    }
}

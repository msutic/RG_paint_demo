using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RG_PaintDemo
{
    public interface ICommandHandler
    {
        event EventHandler CommandExecuted;

        void Execute(ICommand command);
        void Undo();
        void Redo();
        bool CanUndo();
        bool CanRedo();
    }
}

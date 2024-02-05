using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CoursWork_Etap1.Utilities.Interfaces
{
    public interface IAction
    {
        void Execute();
        void Undo();

    }
}

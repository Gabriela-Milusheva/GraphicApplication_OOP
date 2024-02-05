using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;

namespace CoursWork_Etap1.Utilities.Interfaces
{
    public interface IDeserializeService
    {
        List<Shape> DeserializeSave(string filePath);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;

namespace CoursWork_Etap1.Utilities
{
    public interface ISelectShapeService
    {
        void SelectAllShapes(List<Shape> _shapes);

        void SelectAllShapesByType(List<Shape> _shapes, Type type);
    }
}

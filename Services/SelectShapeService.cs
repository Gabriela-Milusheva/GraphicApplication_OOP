using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities;

namespace CoursWork_Etap1.Services
{
    public class SelectShapeService : ISelectShapeService
    {
        public void SelectAllShapes(List<Shape> _shapes) => _shapes.ForEach(s => s.IsSelected = true);

        public void SelectAllShapesByType(List<Shape> _shapes, Type type)
        {
            foreach (Shape shape in _shapes)
            {
                if (shape.GetType() == type)
                {
                    shape.IsSelected = true;
                }
                else shape.IsSelected = false;
            }
        }

    }
}

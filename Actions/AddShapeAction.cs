using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1.Services
{
    public class AddShapeAction : IAction
    {
        private Shape _shape;
        private List<Shape> _shapes;

        public AddShapeAction(Shape shape, List<Shape> shapes)
        {
            _shape = shape;
            _shapes = shapes;
        }

        public void Execute()
        {
            _shapes.Add(_shape);
        }

        public void Undo()
        {
            _shapes.Remove(_shape);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1.Services
{
    public class CopyPasteShapeAction : IAction
    {
        private Shape _shape;
        private Shape _newShape;
        private List<Shape> _shapes;
        private Vector2 _newLocation;

        public CopyPasteShapeAction(Shape shape, List<Shape> shapes, Vector2 newLocation)
        {
            _shape = shape;
            _shapes = shapes;
            _newLocation = newLocation;
            _newShape = _shape.Clone(_newLocation);
        }

        public void Execute()
        {
        
            _shapes.Add(_newShape);
        }

        public void Undo()
        {
            _shapes.Remove(_newShape);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1.Services
{
    public class ClearAllShapesAction : IAction
    {
        private List<Shape> _shapes;
        private List<Shape> _shapesCopy;

        public ClearAllShapesAction(List<Shape> shapes)
        {
            _shapes = shapes;
            _shapesCopy = new List<Shape>(_shapes); //making a copy of the shapes list
        }

        public void Execute()
        {
            _shapes.Clear();
        }

        public void Undo()
        {
            _shapes.AddRange(_shapesCopy);
        }
    }
}

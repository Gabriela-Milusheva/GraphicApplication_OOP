using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1.Services
{
    public class DeleteShapeAction : IAction
    {
        private List<Shape> _shapes;
        private List<Shape> _selectedShapes;

        public DeleteShapeAction(List<Shape> shapes)
        {
            _shapes = shapes;
            _selectedShapes = _shapes.Where(s => s.IsSelected).ToList();
        }

        public void Execute()
        {
            _shapes.RemoveAll(s => s.IsSelected);

        }

        public void Undo()
        {           
            _shapes.AddRange(_selectedShapes);

            foreach (var shape in _selectedShapes)
            {
                shape.IsSelected = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using System.Windows.Forms;
using CoursWork_Etap1.Utilities;
using CoursWork_Etap1.Utilities.Interfaces;
using System.Drawing;

namespace CoursWork_Etap1.Services
{
    public class MoveShapeAction : IAction
    {

        private List<Shape> _selectedShapes;
        private Vector2 _offset;

        public MoveShapeAction(List<Shape> selectedShapes, Vector2 offset)
        {
            _selectedShapes = selectedShapes;
            _offset = offset;
        }

        public void Execute()
        {
            _selectedShapes.ForEach(shape => shape.Move(_offset));
        }
       
        public void Undo()
        {
            _selectedShapes.ForEach(shape => shape.Move(-_offset));
        }
    }
}

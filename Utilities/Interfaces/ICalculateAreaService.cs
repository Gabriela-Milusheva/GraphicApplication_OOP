using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;

namespace CoursWork_Etap1.Utilities.Interfaces
{
    public interface ICalculateAreaService
    {
        double TotalAreaOfAllShapes(List<Shape> shapes);
        double TotalAreaOfAllShapesFromType(List<Shape> shapes, Type type);
        double BiggestAreaOfAllShapes(List<Shape> shapes);
        double BiggestAreaFromType(List<Shape> shapes, Type type);
        double SmallestAreaOfAllShapes(List<Shape> shapes);
        double SmallestAreaFromType(List<Shape> shapes, Type type);

    }
}

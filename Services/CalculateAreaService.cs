using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;

namespace CoursWork_Etap1.Services
{
    public class CalculateAreaService : ICalculateAreaService
    {
        public double TotalAreaOfAllShapes(List<Shape> shapes) => shapes.Sum(s => s.Area);

        public double TotalAreaOfAllShapesFromType(List<Shape> shapes, Type type) =>
            shapes
                .Where(s => s.GetType() == type)
                .Select(s => s.Area)
                .Sum();

        public double BiggestAreaOfAllShapes(List<Shape> shapes) =>
            shapes
                .Select(s => s.Area)
                .Max();

        public double BiggestAreaFromType(List<Shape> shapes, Type type) =>
            shapes
                .Where(s => s.GetType() == type)
                .OrderBy(s => s.Area)
                .Last()
                .Area;

        public double SmallestAreaOfAllShapes(List<Shape> shapes) =>
            shapes
                   .Where(s => s.Area > 0)
                   .Select(s => s.Area)
                   .Min();

        public double SmallestAreaFromType(List<Shape> shapes, Type type) =>
            shapes
               .Where(s => s.GetType() == type && s.Area > 0)
               .Select(s => s.Area)
               .OrderBy(area => area)
               .FirstOrDefault();
    }
}

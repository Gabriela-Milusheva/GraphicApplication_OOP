using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;

namespace CoursWork_Etap1.Utilities.Interfaces
{
    public interface ISerializeShapeService
    {
        void SerializeSave(List<Shape> shapes, string filePath);
        void SerializeToTxtFile(List<Shape> shapes, string filePath);
        void SerializeToJsonFile(List<Shape> shapes, string filePath);
        void SerializeToXmlFile(List<Shape> shapes, string filePath);
        void SerializeToJpegFile(Bitmap bitmap, string filePath);
    }
}

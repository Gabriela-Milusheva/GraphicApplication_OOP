using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using CoursWork_Etap1.Utilities;
using CoursWork_Etap1.Constants;
using System.Drawing.Imaging;
using System.IO.Pipes;
using System.Runtime.Serialization;

namespace CoursWork_Etap1.Services
{
    public class SerializeShapeService : ISerializeShapeService
    {
        public SerializeShapeService()
        {
            //-създаване на директорията за съхранение на файловете, ако все още не съществува такава
            Directory.CreateDirectory(FileLocation.FilesFolderPath);
        }

        public void SerializeSave(List<Shape> shapes, string filePath)
        {
            //използване на BinaryFormatter за сериализация на обектите
            var formatter = new BinaryFormatter();

            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                //сериализиране на обектите във файлов поток
                formatter.Serialize(fileStream, shapes);
            }
        }

        public void SerializeToTxtFile(List<Shape> shapes, string filePath)
        {
            string text = new StringBuilder()
                .AppendLine($"Shapes count: {shapes.Count}")
                .Append(Constant.ShapeSeparatorTxt)
                .Append(shapes.Select(s => s.AsString())
                    .Aggregate((f, s) =>
                        f + Constant.ShapeSeparatorTxt + s))
                .AppendLine(Constant.ShapeSeparatorTxt)
                .ToString();

            File.WriteAllText(filePath, text);

            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.Write(text);
            }
        }
        public void SerializeToJsonFile(List<Shape> shapes, string filePath)
        {
            string json = JsonConvert.SerializeObject(shapes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


        public void SerializeToXmlFile(List<Shape> shapes, string filePath)
        {
            Type[] extraTypes = new Type[]
                { typeof(EquilateralTriangle), typeof(Shapes.Rectangle), typeof(Circle), typeof(Square),typeof(Vector2), typeof(Color) };

            XmlSerializer xmlSerializer = new XmlSerializer(shapes.GetType(), extraTypes);

            using (FileStream stream =
                   new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(stream, shapes);
            }
        }

        public void SerializeToJpegFile(Bitmap bitmap, string filePath)
        {
            bitmap.Save(filePath, ImageFormat.Jpeg);
        }

        
    }
}

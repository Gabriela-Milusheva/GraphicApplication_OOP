using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CoursWork_Etap1.Constants;
using CoursWork_Etap1.Shapes;
using CoursWork_Etap1.Utilities.Interfaces;
using Newtonsoft.Json;

namespace CoursWork_Etap1.Services
{
    public class DeserializeService : IDeserializeService
    {
        public List<Shape> DeserializeSave(string filePath)
        {
            List<Shape> shapes;

            var formatter = new BinaryFormatter();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
               shapes = formatter.Deserialize(stream) as List<Shape>;
            }

            return shapes;
        }     
    }
}

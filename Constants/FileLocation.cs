using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWork_Etap1.Constants
{
    public static class FileLocation
    {

        private static readonly string ProjectDirectory =
            Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName;

        public static readonly string FilesFolderPath = Path.Combine(ProjectDirectory, "ShapeFiles");

        public static readonly string FileLocationBinary = Path.Combine(FilesFolderPath, "binary_data.txt");

        public static readonly string FileLocationTxt = Path.Combine(FilesFolderPath, "shapes.txt");

        public static readonly string FileLocationJson = Path.Combine(FilesFolderPath, "shapes.json");

        public static readonly string FileLocationXml = Path.Combine(FilesFolderPath, "shapes.xml");

        public static readonly string FileLocationJpeg = Path.Combine(FilesFolderPath, "shapes.jpeg");
    }
}

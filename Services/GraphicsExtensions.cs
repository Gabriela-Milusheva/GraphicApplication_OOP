using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public static class GraphicsExtensions
{
    public static float Height { get; private set; }

    public static void SetParameters(this Graphics graphics, float height)
    {
        Height = height;
    }

    public static void SetTransform(this Graphics graphics)
    {
        graphics.PageUnit = GraphicsUnit.Millimeter;
        graphics.TranslateTransform(0, Height);
        graphics.ScaleTransform(1.0f, -1.0f);
    }
}

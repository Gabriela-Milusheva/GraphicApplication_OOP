using System;
using System.Drawing;

namespace CoursWork_Etap1.Shapes
{
    //атрибут показващ,че класът е серелизуем
    [Serializable]
    public abstract class Shape 
    {
        public Vector2 Location { get; set; }
        public Color ColorBorder { get; set; }
        public Color FillColor { get; set; }
        public bool IsSelected { get; set; } = false;

        public abstract double Area { get; }
        public abstract string Type { get; }
    
        public abstract bool ContainsPoint(PointF point);

        protected Shape()
        {
        }
        protected Shape(double locationX, double locationY, Color colorBorder, Color fillColor)
        {
            Location = new Vector2(locationX, locationY);
            ColorBorder = colorBorder;
            FillColor = fillColor;
        }

        public abstract Shape Clone(Vector2 NewLocation);
        public abstract void Move(Vector2 newLocation);

        public abstract string AsString();

        public abstract void DrawShape(Graphics graphics, Pen pen, Brush brush);
    }
}

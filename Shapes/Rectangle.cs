using System;
using System.Drawing;
using System.Text;

namespace CoursWork_Etap1.Shapes
{
    [Serializable]
    public class Rectangle : Shape
    {
        public Vector2 TopLeft { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }

        public Rectangle() : base()
        {
        }
        public Rectangle(double topLeftX, double topLeftY, double length, double height, Color colorBorder, Color fillColor)
            : base(topLeftX, topLeftY, colorBorder, fillColor)
        {
            TopLeft = new Vector2(topLeftX, topLeftY);
            Length = length;
            Height = height;
        }
    
        public override bool ContainsPoint(PointF point)
        {
            //cheking if the given point is within the bounds of the rectangle
            return point.X >= TopLeft.X && point.X <= TopLeft.X + Length
                && point.Y >= TopLeft.Y && point.Y <= TopLeft.Y + Height;
        }

        public override double Area => Height * Length;
        public override string Type => "Rectangle";

        public override Shape Clone(Vector2 NewTopLeft)
        {
            return new Rectangle(NewTopLeft.X, NewTopLeft.Y, Length, Height, ColorBorder, FillColor);
        }
        public override void Move(Vector2 d)
        {
            TopLeft = new Vector2(TopLeft.X + d.X, TopLeft.Y + d.Y);
        }

        public override string AsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Rectangle")
                .Append($"Location: {this.TopLeft.AsString()}")
                .AppendLine($"Width: {this.Length}")
                .AppendLine($"Height: {this.Height}")
                .AppendLine($"Area: {this.Area}")
                .AppendLine($"BorderColor: {this.ColorBorder.ToString()}")
                .AppendLine($"FillColor: {this.FillColor.ToString()}")
                .AppendLine($"Is Selected: {this.IsSelected.ToString()}");

            return sb.ToString();
        }

        public override void DrawShape(Graphics graphics, Pen pen, Brush brush)
        {
            float length = (float)Length;
            float height = (float)Height;
            float x = ((float)TopLeft.X);
            float y = ((float)TopLeft.Y);

            graphics.SetTransform();
            if (IsSelected)
            {
                Pen selectPen = new Pen(Color.Gray, 0.1f) { DashPattern = new float[] { 1, 1 } };
                graphics.FillRectangle(brush, x, y, length, height);
                graphics.DrawRectangle(selectPen, x, y, length, height);
            }
            else
            {
                graphics.FillRectangle(brush, x, y, length, height);
                graphics.DrawRectangle(pen, x, y, length, height);
            }      
            graphics.ResetTransform();
        }
    }
}

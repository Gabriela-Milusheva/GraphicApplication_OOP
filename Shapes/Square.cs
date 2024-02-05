using System;
using System.Drawing;
using System.Text;

namespace CoursWork_Etap1.Shapes
{
      [Serializable]
    public class Square : Shape
    {
        public Vector2 TopLeft { get; set; }
        public double SideLength { get; set; }


        public Square() : base()
        {
        }
        public Square(double topLeftX, double topLeftY, double sideLength, Color colorBorder, Color fillColor)
            : base(topLeftX, topLeftY, colorBorder, fillColor)

        {
            TopLeft = new Vector2(topLeftX, topLeftY);
            SideLength = sideLength;
        }
     
        public override bool ContainsPoint(PointF point)
        {
            //cheking if the point is within the bounds of the square
            return point.X >= TopLeft.X && point.X <= TopLeft.X + SideLength
                && point.Y >= TopLeft.Y && point.Y <= TopLeft.Y + SideLength;
        }

        public override double Area => SideLength * SideLength;
        public override string Type => "Square";
      
        public override Shape Clone(Vector2 NewTopLeft)
        {
            return new Square(NewTopLeft.X, NewTopLeft.Y, SideLength, ColorBorder, FillColor);
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
                .AppendLine($"Side: {this.SideLength}")
                .AppendLine($"Area: {this.Area}")
                .AppendLine($"BorderColor: {this.ColorBorder.ToString()}")
                .AppendLine($"FillColor: {this.FillColor.ToString()}")
                .AppendLine($"Is Selected: {this.IsSelected.ToString()}");

            return sb.ToString();
        }

        public override void DrawShape(Graphics graphics, Pen pen, Brush brush)
        {
            float side = (float)SideLength;
            float x = ((float)TopLeft.X);
            float y = ((float)TopLeft.Y);

            graphics.SetTransform();
            if (IsSelected)
            {
                Pen selectPen = new Pen(Color.Gray, 0.1f) { DashPattern = new float[] { 1, 1 } };
                graphics.FillRectangle(brush, x, y, side, side);
                graphics.DrawRectangle(selectPen, x, y, side, side);
            }
            else
            {
                graphics.FillRectangle(brush, x, y, side, side);
                graphics.DrawRectangle(pen, x, y, side, side);
            }         
            graphics.ResetTransform();
        }
    }
}

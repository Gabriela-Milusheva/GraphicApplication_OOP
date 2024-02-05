using System;
using System.Drawing;
using System.Text;

namespace CoursWork_Etap1.Shapes
{
    [Serializable]
    public class EquilateralTriangle :Shape
    {
        public Vector2 FirstV { get; set; }
        public double Side { get; set; }

        public EquilateralTriangle() : base()
        {
        }
        public EquilateralTriangle(double firstVX, double firstVY, double side, Color colorBorder, Color fillColor)
             : base(firstVX, firstVY, colorBorder, fillColor)
        {
            this.Side = side;
            this.FirstV = new Vector2(firstVX, firstVY);
        }
      
        public override bool ContainsPoint(PointF point)
        {
            float side = (float)Side;
            PointF[] points = new PointF[3];
            points[0] = new PointF((float)FirstV.X, (float)FirstV.Y);
            points[1] = new PointF((float)(FirstV.X - side / 2), (float)(FirstV.Y - (Math.Sqrt(3) / 2) * side));
            points[2] = new PointF((float)(FirstV.X + side / 2), (float)(FirstV.Y - (Math.Sqrt(3) / 2) * side));

            //calculating the barycentric coordinates
            float denom = ((points[1].Y - points[2].Y) * (points[0].X - points[2].X) + (points[2].X - points[1].X) * (points[0].Y - points[2].Y));
            float a = ((points[1].Y - points[2].Y) * (point.X - points[2].X) + (points[2].X - points[1].X) * (point.Y - points[2].Y)) / denom;
            float b = ((points[2].Y - points[0].Y) * (point.X - points[2].X) + (points[0].X - points[2].X) * (point.Y - points[2].Y)) / denom;
            float c = 1 - a - b;

            //checking if the given point is inside the triangle
            return a > 0 && b > 0 && c > 0;
        }

        public double DistanceToPoint(PointF point)
        {
            double distance = Math.Sqrt(Math.Pow((point.X - FirstV.X), 2) + Math.Pow((point.Y - FirstV.Y), 2));
            return distance;
        }

        public override double Area => (Math.Sqrt(3) * Side * Side) / 4;
        public override string Type => "Equilateral Triangle";

        public override Shape Clone(Vector2 NewFirstV)
        {
            return new EquilateralTriangle(NewFirstV.X, NewFirstV.Y, Side, ColorBorder, FillColor);
        }
        public override void Move(Vector2 d)
        {
            FirstV = new Vector2(FirstV.X + d.X, FirstV.Y + d.Y);
        }

        public override string AsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Equilateral Triangle")
                .Append($"Location: {this.FirstV.AsString()}")
                .AppendLine($"Side: {this.Side}")
                .AppendLine($"Area: {this.Area}")
                .AppendLine($"BorderColor: {this.ColorBorder.ToString()}")
                .AppendLine($"FillColor: {this.FillColor.ToString()}")
                .AppendLine($"Is Selected: {this.IsSelected.ToString()}");

            return sb.ToString();
        }

        public override void DrawShape(Graphics graphics, Pen pen, Brush brush)
        {
            float side = (float)Side;
            PointF[] points = new PointF[3];
            points[0] = new PointF((float)(FirstV.X), (float)FirstV.Y);
            points[1] = new PointF((float)(FirstV.X - side / 2), (float)(FirstV.Y - (Math.Sqrt(3) / 2) * side));
            points[2] = new PointF((float)(FirstV.X + (side / 2)), (float)(FirstV.Y - (Math.Sqrt(3) / 2) * side));

            graphics.SetTransform();
            if (IsSelected)
            {
                Pen selectPen = new Pen(Color.Gray, 0.1f) { DashPattern = new float[] { 1, 1 } };
                graphics.FillPolygon(brush, points);
                graphics.DrawPolygon(selectPen, points);
            }
            else
            {
                graphics.FillPolygon(brush, points);
                graphics.DrawPolygon(pen, points);
            }          
            graphics.ResetTransform();
        }
    }
} 
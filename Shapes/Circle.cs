using System;
using System.Drawing;
using System.Text;

namespace CoursWork_Etap1.Shapes
{
    [Serializable]
    public class Circle : Shape
    {
        public Vector2 Center { get; set; }
        public double Radius { get; set; }
        public double Diameter => Radius * 2.0;

        public Circle() : base()
        {
        }

        public Circle(double centerX, double centerY, double radius, Color colorBorder, Color fillColor)
              : base(centerX, centerY, colorBorder, fillColor)  
        {
            this.Center = new Vector2(centerX, centerY);
            this.Radius = radius;
        }    

        public override bool ContainsPoint(PointF point)
        {
            //cheking if the given point is within the bounds of the circle
            double distance = Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
            return distance <= Radius;
        }

        public double DistanceToPoint(PointF point)
        {
            double distance = Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
            return distance - Radius;
        }

        public override double Area => Math.PI * Radius * Radius;
        public override string Type => "Circle";

        public override Shape Clone(Vector2 NewCenter)
        {
            return new Circle(NewCenter.X, NewCenter.Y, Radius, ColorBorder, FillColor);
        }

        public override void Move(Vector2 d)
        {
            Center = new Vector2(Center.X + d.X, Center.Y + d.Y);
        }

        public override string AsString()
        {
            StringBuilder sb = new StringBuilder();

            //добавя се информацията за кръга към обекта от тип StringBuilder
            sb.AppendLine("Circle")
                .Append($"Location: {this.Center.AsString()}")
                .AppendLine($"Radius: {this.Radius}")
                .AppendLine($"Area: {this.Area}")
                .AppendLine($"BorderColor: {this.ColorBorder.ToString()}")
                .AppendLine($"FillColor: {this.FillColor.ToString()}")
                .AppendLine($"Is Selected: {this.IsSelected.ToString()}");

            //накрая методът връща низовото представяне на обекта от тип StringBuilder
            return sb.ToString();
        }

        public override void DrawShape(Graphics graphics, Pen pen, Brush brush)
        {
            float x = (float)(Center.X - Radius);
            float y = (float)(Center.Y - Radius);
            float d = (float)Diameter;

            graphics.SetTransform();
            if(IsSelected)
            {
                Pen selectPen = new Pen(Color.Gray, 0.1f) { DashPattern = new float[] { 1, 1 } };
                graphics.FillEllipse(brush, x, y, d, d);
                graphics.DrawEllipse(selectPen, x, y, d, d);
            }
            else
            {
                graphics.FillEllipse(brush, x, y, d, d);
                graphics.DrawEllipse(pen, x, y, d, d);
            }
            graphics.ResetTransform();
        }

    }
}

using System;
using System.Drawing;
using System.Text;

namespace CoursWork_Etap1
{
    [Serializable]
    public class Vector2
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Vector2()
        {
        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PointF ToPointF(Vector2 v)
        {
            return new PointF((float)v.X, (float)v.Y);
        }
        
        public double DistanceFrom(Vector2 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public Vector2 CopyOrMove(Vector2 fromPoint, Vector2 toPoint)
        {
            double dx = toPoint.X - fromPoint.X;
            double dy = toPoint.Y - fromPoint.Y;

            return new Vector2(this.X + dx, this.Y + dy);
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public virtual string AsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine()
                .AppendLine($"\t X coordinate: {this.X}")
                .AppendLine($"\t Y coordinate: {this.Y}");

            return sb.ToString();
        }
    }
}

using System;

namespace PhongSimulator
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Normalize()
        {
            var length = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (length != 0 && !Double.IsNaN(length))
            {
                X = X / length;
                Y = Y / length;
                Z = Z / length;
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }
        }

        public double Multiply(Vector vector)
        {
            return vector.X * X + vector.Y * Y + vector.Z * Z;
        }

        public Vector Multiply(double value)
        {
            return new Vector(X * value, Y * value, Z * value);
        }
    }
}

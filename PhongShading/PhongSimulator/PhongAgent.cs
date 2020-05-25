using System;
using System.Diagnostics;

namespace PhongSimulator
{
    public class PhongAgent
    {
        public double Ks { get; set; }
        public double Kd { get; set; }
        public double N { get; set; }

        public Point3D Source = new Point3D(0, 0, 200);
        public Vector Observator = new Vector(0, 0, 200);
        public const double Ia = 10;
        public const double Ip = 60000;
        public const double Ka = 0.4;
        public const double Step = 10;

        public byte[] PhongAlgorithm(byte[] image)
        {
            Observator.Normalize();
            var newImage = new byte[image.Length];
            for (int i = 0; i < image.Length; i += 4)
            {
                if (image[i] != 0 || image[i + 1] != 0 || image[i + 2] != 0)
                {
                    var point = ComputeZ(((i % 1600) / 4) - 250, (i / 1600) - 250);
                    var l = point.ToVector();
                    l.Normalize();
                    var n = ComputeVector(point, Source);
                    n.Normalize();
                    var I = CalculateLightReflection(Scalar(n, l),
                                    CalculateCosAlpha(ComputeVector(Source, point), l), point);
                    newImage[i] = Check(image[i] + I);
                    newImage[i + 1] = Check(image[i + 1] + I);
                    newImage[i + 2] = Check(image[i + 2] + I);
                }
                newImage[i + 3] = 255;
            }
            return newImage;
        }

        public void MoveRight()
        {
            Source.X = Source.X - Step;
        }

        public void MoveLeft()
        {
            Source.X = Source.X + Step;
        }

        public void Forward()
        {
            Source.Z = Source.Z - Step;
        }

        public void Backward()
        {
            Source.Z = Source.Z + Step;
        }

        public void Up()
        {
            Source.Y = Source.Y - Step;
        }

        public void Down()
        {
            Source.Y = Source.Y + Step;

        }

        private byte Check(double i)
        {
            if (i < 0)
            {
                return 0;
            }
            else if (i > 255)
            {
                return 255;
            }
            else
            {
                return (byte)i;
            }
        }

        private double CalculateLightReflection(double scalar, double cosAlpha, Point3D point)
        {
            return Ia * Ka
                    + Fatt(point) * Ip * Kd * scalar
                    + Fatt(point) * Ip * Ks * Math.Pow(cosAlpha, N);
        }

        private Point3D ComputeZ(int x, int y)
        {
            return new Point3D(x, y, (int)Math.Sqrt(150 * 150 - x * x - y * y));
        }

        private Vector ComputeVector(Point3D start, Point3D end)
        {
            return new Vector(end.X - start.X, end.Y - start.Y, end.Z - start.Z);
        }

        private double Scalar(Vector v, Vector b)
        {
            return v.X * b.X + v.Y * b.Y + v.Z * b.Z;
        }

        private double CalculateCosAlpha(Vector v, Vector b)
        {
            var distance = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z) * Math.Sqrt(b.X * b.X + b.Y * b.Y + b.Z * b.Z);
            if (Scalar(v, b) > 0) return 0;
            return Scalar(v, b) / distance;
        }

        private double Fatt(Point3D p)
        {
            var distance = Math.Pow(p.X + Source.X, 2) + Math.Pow(p.Y + Source.Y, 2) + Math.Pow(p.Z + Source.Z, 2);
            return 1.0 / Math.Sqrt(distance);
        }

    }
}

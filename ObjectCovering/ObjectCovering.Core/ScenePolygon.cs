using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ObjectCovering.Core
{
    public class ScenePolygon
    {
        public List<List<double>> Points { get; set; }
        public List<List<double>> Points2D { get; set; }
        public double ZMean { get; set; }
        public Brush FillColor { get; set; }

        public void AdjustToCameraPos(int[] camera)
        {
            foreach (var point in Points)
            {
                for (int i = 0; i < point.Count; i++)
                {
                    point[i] -= camera[i];
                }
            }
        }

        public void CalculateZMean(int distance)
        {
            foreach (var point in Points)
            {
                ZMean += point[2];
            }
            ZMean /= Points.Count;
            ZMean -= distance;
        }

        public void From3Dto2D(int distance)
        {
            Points2D = new List<List<double>>();
            foreach (var point in Points)
            {
                double tempZ = point[2];
                if (tempZ <= 0)
                {
                    tempZ = 0.00000001;
                }
                var point2D = new List<double> {
                    point[0] * (distance/tempZ),
                    point[1] * (distance/tempZ)
                };
                Points2D.Add(point2D);
            }
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "Right":
                    foreach (var point in Points)
                    {
                        point[0] += 10;
                    }
                    break;
                case "Left":
                    foreach (var point in Points)
                    {
                        point[0] -= 10;
                    }
                    break;
                case "Up":
                    foreach (var point in Points)
                    {
                        point[1] += 10;
                    }
                    break;
                case "Down":
                    foreach (var point in Points)
                    {
                        point[1] -= 10;
                    }
                    break;
                case "Forwards":
                    foreach (var point in Points)
                    {
                        point[2] -= 10;
                    }
                    break;
                case "Backwards":
                    foreach (var point in Points)
                    {
                        point[2] += 10;
                    }
                    break;
            }
        }

        public void Rotate(string axis, int direction)
        {
            double angle = direction * (Math.PI / 30);
            switch (axis)
            {
                case "Y":
                    foreach (var point in Points)
                    {
                        double temp = point[0];
                        point[0] = temp * Math.Cos(angle) + point[2] * Math.Sin(angle);
                        point[2] = temp * (-Math.Sin(angle)) + point[2] * Math.Cos(angle);
                    }
                    break;
                case "X":
                    foreach (var point in Points)
                    {
                        double temp = point[1];
                        point[1] = temp * Math.Cos(angle) + point[2] * (-Math.Sin(angle));
                        point[2] = temp * Math.Sin(angle) + point[2] * Math.Cos(angle);
                    }
                    break;
                case "Z":
                    foreach (var point in Points)
                    {
                        double temp = point[0];
                        point[0] = temp * Math.Cos(angle) + point[1] * (-Math.Sin(angle));
                        point[1] = temp * Math.Sin(angle) + point[1] * Math.Cos(angle);
                    }
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var point in Points)
            {
                stringBuilder.Append(" [ ");
                foreach (double number in point)
                {
                    stringBuilder.Append($" {number} ");
                }
                stringBuilder.Append(" ] ");
            }
            return stringBuilder.ToString();
        }
    }
}

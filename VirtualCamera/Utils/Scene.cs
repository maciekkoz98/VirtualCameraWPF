using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCamera.Utils
{
    class Scene
    {
        private int distance;

        public int[] CameraPos { get; set; }
        public List<List<double>> Cuboid1 { get; set; }
        public List<List<double>> Cuboid2 { get; set; }
        public List<List<double>> Cuboid3 { get; set; }
        public List<List<double>> Cuboid4 { get; set; }
        public List<List<double>> Points2D { get; set; }

        public void MakeCameraPosZeroPoint()
        {
            for (int i = 0; i < Cuboid1.Count; i++)
            {
                for (int j = 0; j < Cuboid1[i].Count; j++)
                {
                    Cuboid1[i][j] = Cuboid1[i][j] - CameraPos[j];
                    Cuboid2[i][j] = Cuboid2[i][j] - CameraPos[j];
                    Cuboid3[i][j] = Cuboid3[i][j] - CameraPos[j];
                    Cuboid4[i][j] = Cuboid4[i][j] - CameraPos[j];
                }
            }
            distance = -CameraPos[2];
        }

        public void From3Dto2D()
        {
            Points2D = new List<List<double>>();
            double tempZ;
            for (int i = 0; i < Cuboid1.Count; i++)
            {
                tempZ = Cuboid1[i][2];
                if (tempZ <= 0)
                {
                    tempZ = 0.00000001;
                }
                List<double> point2D = new List<double>
                {
                    Cuboid1[i][0] * (distance / tempZ),
                    Cuboid1[i][1] * (distance / tempZ)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid2.Count; i++)
            {
                tempZ = Cuboid2[i][2];
                if (tempZ <= 0)
                {
                    tempZ = 0.00000001;
                }
                List<double> point2D = new List<double>
                {
                    Cuboid2[i][0] * (distance / tempZ),
                    Cuboid2[i][1] * (distance / tempZ)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid3.Count; i++)
            {
                tempZ = Cuboid3[i][2];
                if (tempZ <= 0)
                {
                    tempZ = 0.00000001;
                }
                List<double> point2D = new List<double>
                {
                    Cuboid3[i][0] * (distance / tempZ),
                    Cuboid3[i][1] * (distance / tempZ)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid4.Count; i++)
            {
                tempZ = Cuboid4[i][2];
                if (tempZ <= 0)
                {
                    tempZ = 0.00000001;
                }
                List<double> point2D = new List<double>
                {
                    Cuboid4[i][0] * (distance / tempZ),
                    Cuboid4[i][1] * (distance / tempZ)
                };
                Points2D.Add(point2D);
            }
        }

        public void MoveCamera(string direction)
        {
            switch (direction)
            {
                case "D":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][0] = Cuboid1[i][0] + 10;
                        Cuboid2[i][0] = Cuboid2[i][0] + 10;
                        Cuboid3[i][0] = Cuboid3[i][0] + 10;
                        Cuboid4[i][0] = Cuboid4[i][0] + 10;
                    }
                    break;
                case "A":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][0] = Cuboid1[i][0] - 10;
                        Cuboid2[i][0] = Cuboid2[i][0] - 10;
                        Cuboid3[i][0] = Cuboid3[i][0] - 10;
                        Cuboid4[i][0] = Cuboid4[i][0] - 10;
                    }
                    break;
                case "W":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][1] = Cuboid1[i][1] - 10;
                        Cuboid2[i][1] = Cuboid2[i][1] - 10;
                        Cuboid3[i][1] = Cuboid3[i][1] - 10;
                        Cuboid4[i][1] = Cuboid4[i][1] - 10;
                    }
                    break;
                case "S":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][1] = Cuboid1[i][1] + 10;
                        Cuboid2[i][1] = Cuboid2[i][1] + 10;
                        Cuboid3[i][1] = Cuboid3[i][1] + 10;
                        Cuboid4[i][1] = Cuboid4[i][1] + 10;
                    }
                    break;
                case "Down":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][2] = Cuboid1[i][2] + 10;
                        Cuboid2[i][2] = Cuboid2[i][2] + 10;
                        Cuboid3[i][2] = Cuboid3[i][2] + 10;
                        Cuboid4[i][2] = Cuboid4[i][2] + 10;
                    }
                    break;
                case "Up":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        Cuboid1[i][2] = Cuboid1[i][2] - 10;
                        Cuboid2[i][2] = Cuboid2[i][2] - 10;
                        Cuboid3[i][2] = Cuboid3[i][2] - 10;
                        Cuboid4[i][2] = Cuboid4[i][2] - 10;
                    }
                    break;
            }
            From3Dto2D();
        }

        public void ZoomScene(string direction)
        {
            switch (direction)
            {
                case "+":
                    distance -= 10;
                    break;
                case "-":
                    if (distance >= -10)
                    {
                        distance = -10;
                        return;
                    }
                    distance += 10;
                    break;
            }
            From3Dto2D();
        }

        public void RotateScene(string axis, int direction)
        {
            double angle = direction * (Math.PI / 30);
            double temp;
            switch (axis)
            {
                case "Y":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        temp = Cuboid1[i][0];
                        Cuboid1[i][0] = temp * Math.Cos(angle) + Cuboid1[i][2] * Math.Sin(angle);
                        Cuboid1[i][2] = temp * (-Math.Sin(angle)) + Cuboid1[i][2] * Math.Cos(angle);
                        temp = Cuboid2[i][0];
                        Cuboid2[i][0] = temp * Math.Cos(angle) + Cuboid2[i][2] * Math.Sin(angle);
                        Cuboid2[i][2] = temp * (-Math.Sin(angle)) + Cuboid2[i][2] * Math.Cos(angle);
                        temp = Cuboid3[i][0];
                        Cuboid3[i][0] = temp * Math.Cos(angle) + Cuboid3[i][2] * Math.Sin(angle);
                        Cuboid3[i][2] = temp * (-Math.Sin(angle)) + Cuboid3[i][2] * Math.Cos(angle);
                        temp = Cuboid4[i][0];
                        Cuboid4[i][0] = temp * Math.Cos(angle) + Cuboid4[i][2] * Math.Sin(angle);
                        Cuboid4[i][2] = temp * (-Math.Sin(angle)) + Cuboid4[i][2] * Math.Cos(angle);
                    }
                    break;
                case "X":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        temp = Cuboid1[i][1];
                        Cuboid1[i][1] = temp * Math.Cos(angle) + Cuboid1[i][2] * (-Math.Sin(angle));
                        Cuboid1[i][2] = temp * Math.Sin(angle) + Cuboid1[i][2] * Math.Cos(angle);
                        temp = Cuboid2[i][1];
                        Cuboid2[i][1] = temp * Math.Cos(angle) + Cuboid2[i][2] * (-Math.Sin(angle));
                        Cuboid2[i][2] = temp * Math.Sin(angle) + Cuboid2[i][2] * Math.Cos(angle);
                        temp = Cuboid3[i][1];
                        Cuboid3[i][1] = temp * Math.Cos(angle) + Cuboid3[i][2] * (-Math.Sin(angle));
                        Cuboid3[i][2] = temp * Math.Sin(angle) + Cuboid3[i][2] * Math.Cos(angle);
                        temp = Cuboid4[i][1];
                        Cuboid4[i][1] = temp * Math.Cos(angle) + Cuboid4[i][2] * (-Math.Sin(angle));
                        Cuboid4[i][2] = temp * Math.Sin(angle) + Cuboid4[i][2] * Math.Cos(angle);
                    }
                    break;
                case "Z":
                    for (int i = 0; i < Cuboid1.Count; i++)
                    {
                        temp = Cuboid1[i][0];
                        Cuboid1[i][0] = temp * Math.Cos(angle) + Cuboid1[i][1] * (-Math.Sin(angle));
                        Cuboid1[i][1] = temp * Math.Sin(angle) + Cuboid1[i][1] * Math.Cos(angle);
                        temp = Cuboid2[i][0];
                        Cuboid2[i][0] = temp * Math.Cos(angle) + Cuboid2[i][1] * (-Math.Sin(angle));
                        Cuboid2[i][1] = temp * Math.Sin(angle) + Cuboid2[i][1] * Math.Cos(angle);
                        temp = Cuboid3[i][0];
                        Cuboid3[i][0] = temp * Math.Cos(angle) + Cuboid3[i][1] * (-Math.Sin(angle));
                        Cuboid3[i][1] = temp * Math.Sin(angle) + Cuboid3[i][1] * Math.Cos(angle);
                        temp = Cuboid4[i][0];
                        Cuboid4[i][0] = temp * Math.Cos(angle) + Cuboid4[i][1] * (-Math.Sin(angle));
                        Cuboid4[i][1] = temp * Math.Sin(angle) + Cuboid4[i][1] * Math.Cos(angle);
                    }
                    break;
            }
            From3Dto2D();
        }
    }
}

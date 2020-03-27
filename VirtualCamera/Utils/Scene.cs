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
        private readonly int distance = 1000;

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
        }

        public void From3Dto2D()
        {
            Points2D = new List<List<double>>();
            for (int i = 0; i < Cuboid1.Count; i++)
            {
                List<double> point2D = new List<double>
                {
                    Cuboid1[i][0] * distance / (Cuboid1[i][2] + distance),
                    Cuboid1[i][1] * distance / (Cuboid1[i][2] + distance)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid2.Count; i++)
            {
                List<double> point2D = new List<double>
                {
                    Cuboid2[i][0] * distance / (Cuboid2[i][2] + distance),
                    Cuboid2[i][1] * distance / (Cuboid2[i][2] + distance)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid3.Count; i++)
            {
                List<double> point2D = new List<double>
                {
                    Cuboid3[i][0] * distance / (Cuboid3[i][2] + distance),
                    Cuboid3[i][1] * distance / (Cuboid3[i][2] + distance)
                };
                Points2D.Add(point2D);
            }
            for (int i = 0; i < Cuboid4.Count; i++)
            {
                List<double> point2D = new List<double>
                {
                    Cuboid4[i][0] * distance / (Cuboid4[i][2] + distance),
                    Cuboid4[i][1] * distance / (Cuboid4[i][2] + distance)
                };
                Points2D.Add(point2D);
            }
        }
    }
}

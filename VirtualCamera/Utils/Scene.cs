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
        private readonly int distance = 500;

        public int[] CameraPos { get; set; }
        public List<List<double>> Cuboid1 { get; set; }
        public List<List<double>> Cuboid2 { get; set; }
        public List<List<double>> Cuboid3 { get; set; }
        public List<List<double>> Cuboid4 { set; get; }

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
            for (int i = 0; i < Cuboid1.Count; i++)
            {
                for (int j = 0; j < Cuboid1[i].Count; j++)
                {
                    Cuboid1[i][j] = Cuboid1[i][j] * distance / (Cuboid1[i][2] + distance);
                    Cuboid2[i][j] = Cuboid2[i][j] * distance / (Cuboid2[i][2] + distance);
                    Cuboid3[i][j] = Cuboid3[i][j] * distance / (Cuboid3[i][2] + distance);
                    Cuboid4[i][j] = Cuboid4[i][j] * distance / (Cuboid4[i][2] + distance);
                }
            }
        }

        public List<double[,]> CreatePointsList()
        {
            List<double[,]> pointsList = new List<double[,]>
            {
                //new double[4, 2] { { Cuboid1[1][0], Cuboid1[1][1] }, { Cuboid1[5][0], Cuboid1[5][1] }, { Cuboid1[6][0], Cuboid1[6][1] }, { Cuboid1[2][0], Cuboid1[2][1] } },
                //new double[4, 2] { { Cuboid1[0][0], Cuboid1[0][1] }, { Cuboid1[4][0], Cuboid1[4][1] }, { Cuboid1[5][0], Cuboid1[5][1] }, { Cuboid1[1][0], Cuboid1[1][1] } },
                //new double[4, 2] { { Cuboid1[3][0], Cuboid1[3][1] }, { Cuboid1[7][0], Cuboid1[7][1] }, { Cuboid1[6][0], Cuboid1[6][1] }, { Cuboid1[2][0], Cuboid1[2][1] } },
                //new double[4, 2] { { Cuboid1[0][0], Cuboid1[0][1] }, { Cuboid1[4][0], Cuboid1[4][1] }, { Cuboid1[7][0], Cuboid1[7][1] }, { Cuboid1[3][0], Cuboid1[3][1] } },
                new double[4, 2] { { Cuboid2[0][0], Cuboid2[0][1] }, { Cuboid2[1][0], Cuboid2[1][1] }, { Cuboid2[2][0], Cuboid2[2][1] }, { Cuboid2[3][0], Cuboid2[3][1] } },
                new double[4, 2] { { Cuboid2[0][0], Cuboid2[0][1] }, { Cuboid2[4][0], Cuboid2[4][1] }, { Cuboid2[5][0], Cuboid2[5][1] }, { Cuboid2[1][0], Cuboid2[1][1] } },
                new double[4, 2] { { Cuboid2[3][0], Cuboid2[3][1] }, { Cuboid2[7][0], Cuboid2[7][1] }, { Cuboid2[6][0], Cuboid2[6][1] }, { Cuboid2[2][0], Cuboid2[2][1] } },
                new double[4, 2] { { Cuboid2[1][0], Cuboid2[1][1] }, { Cuboid2[5][0], Cuboid2[5][1] }, { Cuboid2[6][0], Cuboid2[6][1] }, { Cuboid2[2][0], Cuboid2[2][1] } },
                //new double[4, 2] { { Cuboid3[0][0], Cuboid3[0][1] }, { Cuboid3[1][0], Cuboid3[1][1] }, { Cuboid3[2][0], Cuboid3[2][1] }, { Cuboid3[3][0], Cuboid3[3][1] } },
                //new double[4, 2] { { Cuboid3[0][0], Cuboid3[0][1] }, { Cuboid3[4][0], Cuboid3[4][1] }, { Cuboid3[5][0], Cuboid3[5][1] }, { Cuboid3[1][0], Cuboid3[1][1] } },
                //new double[4, 2] { { Cuboid3[3][0], Cuboid3[3][1] }, { Cuboid3[7][0], Cuboid3[7][1] }, { Cuboid3[6][0], Cuboid3[6][1] }, { Cuboid3[2][0], Cuboid3[2][1] } },
                //new double[4, 2] { { Cuboid3[1][0], Cuboid3[1][1] }, { Cuboid3[5][0], Cuboid3[5][1] }, { Cuboid3[6][0], Cuboid3[6][1] }, { Cuboid3[2][0], Cuboid3[2][1] } },
                //new double[4, 2] { { Cuboid4[0][0], Cuboid4[0][1] }, { Cuboid4[1][0], Cuboid4[1][1] }, { Cuboid4[2][0], Cuboid4[2][1] }, { Cuboid4[3][0], Cuboid4[3][1] } },
                //new double[4, 2] { { Cuboid4[0][0], Cuboid4[0][1] }, { Cuboid4[4][0], Cuboid4[4][1] }, { Cuboid4[5][0], Cuboid4[5][1] }, { Cuboid4[1][0], Cuboid4[1][1] } },
                //new double[4, 2] { { Cuboid4[3][0], Cuboid4[3][1] }, { Cuboid4[7][0], Cuboid4[7][1] }, { Cuboid4[6][0], Cuboid4[6][1] }, { Cuboid4[2][0], Cuboid4[2][1] } },
                //new double[4, 2] { { Cuboid4[1][0], Cuboid4[1][1] }, { Cuboid4[5][0], Cuboid4[5][1] }, { Cuboid4[6][0], Cuboid4[6][1] }, { Cuboid4[2][0], Cuboid4[2][1] } }
            };
            return pointsList;
        }
    }
}

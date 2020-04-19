using System.Collections.Generic;
using System.Linq;

namespace ObjectCovering.Core
{
    public class Scene
    {
        private int distance;

        public List<ScenePolygon> Polygons { get; set; }
        public int[] CameraPos { get; set; }

        public void MakeCameraPosZeroPoint()
        {
            foreach (var polygon in Polygons)
            {
                polygon.AdjustToCameraPos(CameraPos);
            }

            distance = -CameraPos[2];
        }

        public void SortPolygons()
        {
            foreach (var polygon in Polygons)
            {
                polygon.CalculateZMean(distance);
            }
            Polygons = Polygons.OrderByDescending(x => x.ZMean).ToList();
        }

        public void From3Dto2D()
        {
            foreach (var polygon in Polygons)
            {
                polygon.From3Dto2D(distance);
            }
        }

        public void MoveCamera(string direction)
        {
            switch (direction)
            {
                case "D":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Right");
                    }
                    break;
                case "A":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Left");
                    }
                    break;
                case "W":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Up");
                    }
                    break;
                case "S":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Down");
                    }
                    break;
                case "Down":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Backwards");
                    }
                    break;
                case "Up":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Move("Forwards");
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
            switch (axis)
            {
                case "Y":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Rotate("Y", direction);
                    }
                    break;
                case "X":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Rotate("X", direction);
                    }
                    break;
                case "Z":
                    foreach (var polygon in Polygons)
                    {
                        polygon.Rotate("Z", direction);
                    }
                    break;
            }
            From3Dto2D();
        }
    }
}

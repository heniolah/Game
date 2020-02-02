using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class LeeAlg
    {
        List<Point> path;

        int[,] map;

        public List<Point> GetShortestPath(Point startingPoint, Point destPoint, int[,]auxmap)
        {
            path = new List<Point>();
            map = new int[auxmap.GetLength(0), auxmap.GetLength(1)];
            for(int i=0;i<auxmap.GetLength(0);i++)
            {
                for(int j=0;j<auxmap.GetLength(1);j++)
                {
                    map[i, j] = auxmap[i, j];
                }
            }
            path.Add(startingPoint);

            while (path.Count > 0)
            {
                Point currerntPoint = path[0];
                path.RemoveAt(0);

                if (CanGo(currerntPoint, -1, 0))
                {
                    path.Add(new Point(currerntPoint.X - 1, currerntPoint.Y));
                    map[currerntPoint.Y, currerntPoint.X - 1] = map[currerntPoint.Y, currerntPoint.X] + 1;
                }
                if (CanGo(currerntPoint, 1, 0))
                {
                    path.Add(new Point(currerntPoint.X + 1, currerntPoint.Y));
                    map[currerntPoint.Y, currerntPoint.X + 1] = map[currerntPoint.Y, currerntPoint.X] + 1;
                }
                if (CanGo(currerntPoint, 0, -1))
                {
                    path.Add(new Point(currerntPoint.X, currerntPoint.Y - 1));
                    map[currerntPoint.Y - 1, currerntPoint.X] = map[currerntPoint.Y, currerntPoint.X] + 1;
                }
                if (CanGo(currerntPoint, 0, 1))
                {
                    path.Add(new Point(currerntPoint.X, currerntPoint.Y + 1));
                    map[currerntPoint.Y + 1, currerntPoint.X] = map[currerntPoint.Y, currerntPoint.X] + 1;
                }
            }

            path = BacktrackPath(destPoint);
            path.Add(startingPoint);
            return path;
        }

        List<Point> BacktrackPath(Point destPoint)
        {
            List<Point> path = new List<Point>();
            Point currentPoint = destPoint;

            path.Add(currentPoint);

            while (map[currentPoint.Y, currentPoint.X] != 1)
            {
                List<Point> options = new List<Point>();
                if (ShouldGo(currentPoint, -1, 0))
                {
                    options.Add(new Point(currentPoint.X - 1, currentPoint.Y));
                }
                if (ShouldGo(currentPoint, 1, 0))
                {
                    options.Add(new Point(currentPoint.X + 1, currentPoint.Y));
                }
                if (ShouldGo(currentPoint, 0, -1))
                {
                    options.Add(new Point(currentPoint.X, currentPoint.Y - 1));
                }
                if (ShouldGo(currentPoint, 0, 1))
                {
                    options.Add(new Point(currentPoint.X, currentPoint.Y + 1));
                }
                currentPoint = options[Engine.rnd.Next(options.Count)];
                path.Add(currentPoint);
            }
            return path;
        }

        public bool CanGo(Point currentPoint, int xOffset, int yOffset)
        {
            if (currentPoint.X + xOffset >= 0 && currentPoint.Y + yOffset >= 0 &&
                currentPoint.X + xOffset < map.GetLength(1) && currentPoint.Y + yOffset < map.GetLength(0) &&
                map[currentPoint.Y + yOffset, currentPoint.X + xOffset] == 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public bool ShouldGo(Point currentPoint, int xOffset, int yOffset)
        {
            if (currentPoint.X + xOffset >= 0 && currentPoint.Y + yOffset >= 0 &&
                currentPoint.X + xOffset < map.GetLength(1) && currentPoint.Y + yOffset < map.GetLength(0) &&
                map[currentPoint.Y + yOffset, currentPoint.X + xOffset] == map[currentPoint.Y, currentPoint.X] - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Robot
    {
        public Point location;

        public List<Point> path;
        public LeeAlg pathFinder;
        public Robot()
        {
            location = new Point(0, 0);
            pathFinder = new LeeAlg();
            path = new List<Point>();
        }

        public void Work()
        {
            if(path.Count == 0)
            {
                path = pathFinder.GetShortestPath(location, Engine.GetEmptySpace(), Engine.map);
            }
            else
            {
                location = path[path.Count - 1];
                path.RemoveAt(path.Count - 1);
            }
        }
    }
}

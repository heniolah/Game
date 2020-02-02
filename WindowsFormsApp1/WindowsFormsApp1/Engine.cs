using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Engine
    {
        public static Random rnd = new Random();

        public static int[,] map;

        public static PictureBox pictureBox;
        public static Bitmap bmp;
        public static Graphics grp;

        public static Robot robot;

        public static Point GetEmptySpace()
        {
            Point emptyPoint = new Point(rnd.Next(map.GetLength(1)),rnd.Next(map.GetLength(0)));
            while(map[emptyPoint.Y, emptyPoint.X] == -1 || robot.location == emptyPoint)
            {
                emptyPoint = new Point(rnd.Next(map.GetLength(1)), rnd.Next(map.GetLength(0)));
            }
            return emptyPoint;
        }

        public static void Init(PictureBox pictureBox)
        {
            Engine.pictureBox = pictureBox;
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            map = new int[,] { 
            { 0, 0, 0, 0, 0 },
            { 0, -1, 0, -1, 0 },
            { 0, 0, -1, -1, -1 },
            { -1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
            };

            robot = new Robot();
        }

        public static void DrawMap()
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);
            Pen myPen = new Pen(Color.Red);
            for(int i=0;i<map.GetLength(1);i++)
            {
                grp.DrawLine(myPen, new Point(pictureBox.Width / map.GetLength(1) * i, 0), new Point(pictureBox.Width / map.GetLength(1) * i, pictureBox.Height));
            }
            for(int i=0;i<map.GetLength(0);i++)
            {
                grp.DrawLine(myPen, new Point(0, pictureBox.Height / map.GetLength(0) * i), new Point(pictureBox.Width, pictureBox.Height / map.GetLength(0) * i));
            }
            for(int i=0;i<map.GetLength(0);i++)
            {
                for (int j=0;j<map.GetLength(1);j++)
                {
                    if(map[i,j]==-1)
                    {
                        grp.FillRectangle(new SolidBrush(Color.Red), pictureBox.Width / map.GetLength(1) * j, pictureBox.Height / map.GetLength(0) * i, pictureBox.Width / map.GetLength(1), pictureBox.Height / map.GetLength(0));
                    }
                }
            }

            grp.DrawEllipse(myPen, pictureBox.Width / map.GetLength(1) * robot.location.X, pictureBox.Height / map.GetLength(0) * robot.location.Y, pictureBox.Width / map.GetLength(1), pictureBox.Height / map.GetLength(0));
            pictureBox.Image = bmp;
        }

        public static void Run()
        {
            //DrawMap();
            robot.Work();
            DrawMap();
        }
    }
}

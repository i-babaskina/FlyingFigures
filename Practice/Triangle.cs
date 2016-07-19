using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRandom = MyRandomLibrary.MyRandom;
using System.Xml.Serialization;

namespace Practice
{
    [Serializable]
    public class Triangle : Figure
    {
        private int height;
        private int width;
        private Point a;
        private Point b;
        private Point c;

        [NonSerialized]
        object locker = new object();

        public Point[] Points = new Point[3];
        [NonSerialized]
        Pen pen;

        [XmlAttribute]
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        [XmlAttribute]
        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public Point A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public Point B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public Point C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public Triangle(int xMax, int yMax)
        {
            lock (locker)
            {
                Color = MyRandom.GetRandomColor();
                Height = MyRandom.GetRandomPoint(25, 100);
                Width = MyRandom.GetRandomPoint(25, 100);
                X = MyRandom.GetRandomPoint(Width / 2, xMax - Width);
                Y = MyRandom.GetRandomPoint(yMax - Height);
                Dx = MyRandom.GetRandomSpeed();
                Dy = MyRandom.GetRandomSpeed();
            }
        }

        public Triangle() { }

        public override void Draw(Graphics g)
        {
            //lock (locker)
            {
                pen = new Pen(Color, 3);
                A = new Point(X, Y);
                B = new Point(X - (Width / 2), Y + Height);
                C = new Point(X + (Width / 2), Y + Height);
            }
            Points[0] = A;
            Points[1] = B;
            Points[2] = C;
            DrawTriangleLine(g, A, B);
            DrawTriangleLine(g, B, C);
            DrawTriangleLine(g, C, A);

        }

        public override void Move(int xMax, int yMax)
        {
            Validate(xMax, yMax);
            if (!this.IsMoved) return;
            //lock (locker)
            {
                if (B.X <= 0)
                {
                    Dx = -Dx;
                }
                if (C.X >= xMax)
                {
                    Dx = -Dx;
                }

                if (A.Y <= 0)
                {
                    Dy = -Dy;
                }

                if (B.Y >= yMax)
                {
                    Dy = -Dy;
                }

                X += Dx;
                Y += Dy;
            }


        }

        public override void BackToPictureBox(int xMax, int yMax)
        {
            lock (locker)
            {
                if (Height >= yMax || Width >= yMax)
                {
                    Height = MyRandom.GetRandomPoint(25, (int)yMax / 2);
                    Width = MyRandom.GetRandomPoint(25, (int)xMax / 2);
                }
                X = MyRandom.GetRandomPoint(Width / 2, xMax - Width);
                Y = MyRandom.GetRandomPoint(yMax - Height);
            }
        }

        private void DrawTriangleLine(Graphics g, Point a, Point b)
        {
            g.DrawLine(pen, a, b);
        }

        public override void ChangeDirection()
        {
            Dx = -Dx;
            Dy = -Dy;
        }

    }
}

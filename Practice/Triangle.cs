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
            Color = MyRandom.GetRandomColor();
            Height = MyRandom.GetRandomPoint(25, (int)yMax/2);
            Width = MyRandom.GetRandomPoint(25, (int)xMax/2);
            X = MyRandom.GetRandomPoint(Width/2, xMax - Width);
            Y = MyRandom.GetRandomPoint(yMax - Height);
            Dx = MyRandom.GetRandomSpeed();
            Dy = MyRandom.GetRandomSpeed();
        }

        public Triangle() { }

        public override void Draw(Graphics g)
        {
            pen = new Pen(Color);
            A = new Point(X, Y);
            B = new Point(X - (Width / 2), Y + Height);
            C = new Point(X + (Width / 2), Y + Height);
            DrawTriangleLine(g, A, B);
            DrawTriangleLine(g, B, C);
            DrawTriangleLine(g, C, A);
        }

        public override void Move(int xMax, int yMax)
        {
            Validate(xMax, yMax);
            if (!this.IsMoved) return;
            if (B.X <= Math.Abs(Dx) + 1)
            {
                Dx = -Dx;
            }
            if (C.X >= xMax)
            {
                Dx = -Dx;
            }

            if (A.Y <= Math.Abs(Dy) + 1)
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

        public override void BackToPictureBox(int xMax, int yMax)
        {
            if (Height >= yMax || Width >= yMax)
            {
                Height = MyRandom.GetRandomPoint(25, (int)yMax / 2);
                Width = MyRandom.GetRandomPoint(25, (int)xMax / 2);
            }
            X = MyRandom.GetRandomPoint(Width / 2, xMax - Width);
            Y = MyRandom.GetRandomPoint(yMax - Height);
        }

        private void DrawTriangleLine(Graphics g, Point a, Point b)
        {
            g.DrawLine(pen, a, b);
        }


    }
}

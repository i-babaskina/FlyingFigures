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
    public class Circle : Figure
    {
        private int radius;
        [NonSerialized]
        Pen pen;

        [NonSerialized]
        object locker = new object();

        [XmlAttribute]
        public int Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public Circle(int xMax, int yMax)
        {
            //lock (locker)
            {
                Color = MyRandom.GetRandomColor();
                Radius = MyRandom.GetRandomPoint(25, 75);
                X = MyRandom.GetRandomPoint(Radius, xMax - Radius);
                Y = MyRandom.GetRandomPoint(Radius, yMax - Radius);
                Dx = MyRandom.GetRandomSpeed();
                Dy = MyRandom.GetRandomSpeed();
            }

        }

        public Circle() { }
        public override void Draw(Graphics g)
        {
            pen = new Pen(Color, 3);
            //lock(this)
            {
                DrawCircle(g, pen, X, Y, Radius);
            }
        }

        public override void Move(int xMax, int yMax)
        {
            Validate(xMax, yMax);
            if (!this.IsMoved) return;
            //lock (locker)
            {
                if (X - Radius <= 0 || X + Radius >= xMax )
                Dx = -Dx;
            if (Y - Radius <= 0 || Y + Radius >= yMax)
                Dy = -Dy;
            
                X += Dx;
                Y += Dy;
            }
        }

        public override void ChangeDirection()
        {
            Dx = -Dx;
            Dy = -Dy;
        }

        public override void BackToPictureBox(int xMax, int yMax)
        {
            //lock (locker)
            {
                if (radius > xMax / 2 || radius > yMax / 2) radius = MyRandom.GetRandomPoint(25, GetMaxRadius(xMax, yMax));
                X = MyRandom.GetRandomPoint(radius, xMax - Radius);
                Y = MyRandom.GetRandomPoint(radius, yMax - Radius);
            }
        }

        private static void DrawCircle(Graphics g, Pen pen,
                              int centerX, int centerY, int radius)
        {
            int x0 = centerX - (int)(radius);
            int y0 = centerY - (int)(radius);
            int diameter = radius*2;
            g.DrawEllipse(pen, x0, y0, diameter, diameter);
        }

        private static int GetMaxRadius(int xMax, int yMax)
        {
            return (int)(Support.GetMin(xMax, yMax) / 8);
        }


    }
}

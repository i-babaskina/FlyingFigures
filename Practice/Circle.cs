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
            Color = MyRandom.GetRandomColor();
            Radius = MyRandom.GetRandomPoint(25, GetMaxRadius(xMax, yMax));
            X = MyRandom.GetRandomPoint(25, xMax - Radius);
            Y = MyRandom.GetRandomPoint(25, yMax - Radius);
            Dx = MyRandom.GetRandomSpeed();
            Dy = MyRandom.GetRandomSpeed();

        }

        public Circle() { }
        public override void Draw(Graphics g)
        {
            pen = new Pen(Color);
            DrawCircle(g, pen, X, Y, Radius);
        }

        public override void Move(int xMax, int yMax)
        {
            if (!this.IsMoved) return;
            if (X - Radius < Math.Abs(Dx) || X + Radius > xMax - Math.Abs(Dx))
                Dx = -Dx;
            if (Y - Radius < Math.Abs(Dy) || Y + Radius > yMax - Math.Abs(Dy))
                Dy = -Dy;
            X += Dx;
            Y += Dy;
        }



        private static void DrawCircle(Graphics g, Pen pen,
                              int centerX, int centerY, int radius)
        {
            int x0 = centerX - (int)(radius/2);
            int y0 = centerY - (int)(radius/2);
            int diameter = radius*2;
            g.DrawEllipse(pen, x0, y0, diameter, diameter);
        }

        private static int GetMaxRadius(int xMax, int yMax)
        {
            return (int)(Support.GetMin(xMax, yMax) / 4);
        }


    }
}

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
    public class Rectangle : Figure
    {
        [NonSerialized]
        Pen pen;
        private int height;
        private int width;
        private Point[] points;

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
        
        public Point[] Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }

        public Rectangle(int xMax, int yMax)
        {
            Points = new Point[5];
            Color = MyRandom.GetRandomColor();
            Height = MyRandom.GetRandomPoint(yMax / 2);
            Width = MyRandom.GetRandomPoint(xMax / 2);
            X = MyRandom.GetRandomPoint(0, xMax - Width);
            Y = MyRandom.GetRandomPoint(0, yMax - Height);
            Dx = MyRandom.GetRandomSpeed();
            Dy = MyRandom.GetRandomSpeed();
        }

        public Rectangle()
        {
        }

        public override void Draw(Graphics g)
        {
            Points = new Point[5];
            pen = new Pen(Color);
            Points[0] = new Point(X, Y);
            Points[1] = new Point(X + Width, Y);
            Points[2] = new Point(X + Width, Y + Height);
            Points[3] = new Point(X, Y + Height);
            Points[4] = Points[0];
            g.DrawLines(pen, Points);
        }

        public override void Move(int xMax, int yMax)
        {
            if (!this.IsMoved) return;
            if (Points[0].X <= 0 || Points[1].X >= xMax)
                Dx = -Dx;
            if (Points[0].Y <= 0 || Points[2].Y >= yMax)
                Dy = -Dy;
            X += Dx;
            Y += Dy;
        }


    }
}

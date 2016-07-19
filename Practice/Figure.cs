﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Threading;

namespace Practice
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Triangle))]
    [XmlInclude(typeof(Rectangle))]
    [Serializable]
    public abstract class Figure : IDrawable, IMovable
    {
        private volatile int x;
        private volatile int y;
        private int dx;
        private int dy;
        private Color color;
        private int id;
        private bool isMoved = true;
        
        public event EventHandler<CrossFiguresEventArgs> CrossFigures;


        [XmlAttribute]
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        [XmlAttribute]
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }


        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        [XmlAttribute]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [XmlAttribute]
        public bool IsMoved
        {
            get
            {
                return isMoved;
            }

            set
            {
                isMoved = value;
            }
        }

        [XmlAttribute]
        public int Dx
        {
            get
            {
                return dx;
            }

            set
            {
                dx = value;
            }
        }

        [XmlAttribute]
        public int Dy
        {
            get
            {
                return dy;
            }

            set
            {
                dy = value;
            }
        }

        [XmlElement("BackColor")]
        public int BackColorAsArgb
        {
            get { return Color.ToArgb(); }
            set { Color = Color.FromArgb(value); }
        }

        public virtual void Draw(Graphics g)
        {

        }

        public virtual void Move(int xMax, int yMax)
        {

        }

        public virtual void Validate(int xMax, int yMax)
        {
            if (X > xMax || Y > yMax)
                throw new FigureOutOfPictureBoxException(this);
        }

        public virtual void BackToPictureBox(int xMax, int yMax)
        {
            
        }


        void IDrawable.Test()
        {
            throw new NotImplementedException();
        }


        void IMovable.Test()
        {
            throw new NotImplementedException();
        }

        public virtual void ChangeDirection()
        {

        }

        protected virtual void OnCrossFigures(CrossFiguresEventArgs e)
        {
            EventHandler<CrossFiguresEventArgs> temp = Volatile.Read(ref CrossFigures);
            if (temp != null) temp(this, e);
        }

        public void DoCrossFigures(Figure figure1, Figure figure2)
        {
            CrossFiguresEventArgs e = new CrossFiguresEventArgs(figure1, figure2);
            OnCrossFigures(e);
        }

    }
}

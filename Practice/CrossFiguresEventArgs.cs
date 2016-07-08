using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    class CrossFiguresEventArgs: EventArgs
    {
        private readonly int figure1, figure2;
        private readonly Point crossPoint;

        public int Figure1
        {
            get
            {
                return figure1;
            }
        }

        public int Figure2
        {
            get
            {
                return figure2;
            }
        }

        public Point CrossPoint
        {
            get
            {
                return crossPoint;
            }
        }

        public CrossFiguresEventArgs(int figure1, int figure2, Point point)
        {
            this.figure1 = figure1;
            this.figure2 = figure2;
            crossPoint = point;
        }


             
    }
}

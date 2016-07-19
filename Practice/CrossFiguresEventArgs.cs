using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class CrossFiguresEventArgs: EventArgs
    {
        private readonly Figure figure1, figure2;

        public Figure Figure1
        {
            get
            {
                return figure1;
            }
        }

        public Figure Figure2
        {
            get
            {
                return figure2;
            }
        }


        public CrossFiguresEventArgs(Figure figure1, Figure figure2)
        {
            this.figure1 = figure1;
            this.figure2 = figure2;
        }

        public string Print()
        {
            return string.Format("Crossing betwen {0} {1} and {2} {3}", this.figure1.GetType().Name, this.figure1.Id, this.figure2.GetType().Name, this.figure2.Id);
        }


             
    }
}

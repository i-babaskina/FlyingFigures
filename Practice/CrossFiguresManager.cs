using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice
{
    internal class CrossFiguresManager
    {
        public event EventHandler<CrossFiguresEventArgs> CrossFigures;

        protected virtual void OnCrossFigures(CrossFiguresEventArgs e)
        {
            EventHandler<CrossFiguresEventArgs> temp = Volatile.Read(ref CrossFigures);
            if (temp != null) temp(this, e);            
        }

        public void DoCrossFigures(int figure1, int figure2, Point point)
        {
            CrossFiguresEventArgs e = new CrossFiguresEventArgs(figure1, figure2, point);
            OnCrossFigures(e);
        }

    }
}

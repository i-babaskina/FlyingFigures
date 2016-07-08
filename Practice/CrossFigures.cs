using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    sealed class CrossFigures
    {
        public CrossFigures(CrossFiguresManager cfm)
        {
            cfm.CrossFigures += GetCrossFigures;
        }

        private void GetCrossFigures(Object sender, CrossFiguresEventArgs e)
        {
            Console.WriteLine(e.Figure1);
        }

        public void Unregister(CrossFiguresManager cfm)
        {
            cfm.CrossFigures -= GetCrossFigures;
        }
    }
}

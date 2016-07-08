using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    [Serializable]
    public sealed class FigureOutOfPictureBoxException : Exception
    {
        private readonly Figure figure;

        public FigureOutOfPictureBoxException(Figure figure)
        {
            this.figure = figure;
        }

        public override string Message
        {
            get
            {
                return "Figure #" + figure.Id + " with coordinates X=" + figure.X + " and Y=" + figure.Y + " is out of Picture Box.";
            }
        }
    }
}

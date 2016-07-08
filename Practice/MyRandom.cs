using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class MyRandom1
    {

        private static readonly Random random = new Random();
        public static Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        public static int GetRandomPoint(int max)
        {
            return random.Next(0, max);
        }

        public static int GetRandomPoint(int min, int max)
        {
            return random.Next(min, max);

        }

        public static int GetRandomSpeed()
        {
            int res = random.Next(-4, 4);
            if (res == 0) GetRandomSpeed();
            return res;
        }
    }
}

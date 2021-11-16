using System;
using System.Collections.Generic;
using System.Text;

namespace MadUtils
{
    public class CommonUtils
    {
        public static int RandomNumber(int min, int max)
        {
            int seed = (int)DateTime.Now.Ticks;

            Random random = new Random(seed);
            return random.Next(min, max);
        }
    }
}

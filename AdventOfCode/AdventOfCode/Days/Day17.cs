using System.Collections.Generic;

namespace AdventOfCode.Days
{
    public class Day17 : IPuzzle
    {
        private int input = 367;

        public string RunOne()
        {
            int position = 0;

            List<int> values = new List<int> { 0 };

            for (int i = 1; i < 2018; i++)
            {
                position = (position + input) % i;
                values.Insert(position + 1, i);
                position++;
            }

            return values[values.IndexOf(2017) + 1].ToString();
        }

        public string RunTwo()
        {
            int position = 0;
            int lastValue = -1;

            for (int i = 1; i < 50000001; i++)
            {
                position = (position + input) % i;

                if(position == 0)
                {
                    lastValue = i;
                }

                position++;
            }

            return lastValue.ToString();
        }
    }
}

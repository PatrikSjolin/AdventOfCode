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

            for (int i = 1; i <= 2017; i++)
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
            int lastValue = 0;

            for (int i = 1; i <= 50000000; i++)
            {
                if(i > input)
                {
                    if (position + input >= i)
                    {
                        position = (position + input) - i;
                    }
                    else
                    {
                        int jumps = (i - position) / (input + 1);
                        position += (input + 1) * jumps;
                        i += jumps - 1;
                        position--;
                    }
                }
                else
                {
                    position = (position + input) % i;
                }

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

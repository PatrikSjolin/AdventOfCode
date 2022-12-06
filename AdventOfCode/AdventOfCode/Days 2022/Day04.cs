using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2022
{
    internal class Day04 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input04.txt").ToList();

            int sum = 0;

            for(int i = 0; i < inputs.Count; i++)
            {
                var split = inputs[i].Split(',').ToList();

                int a = int.Parse(split[0].Split('-')[0]);
                int b = int.Parse(split[0].Split('-')[1]);
                int c = int.Parse(split[1].Split('-')[0]);
                int d = int.Parse(split[1].Split('-')[1]);

                if(c >= a && d <= b)
                {
                    sum++;
                }
                else if(a >= c && b <= d)
                {
                    sum++;
                }
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input04.txt").ToList();

            int sum = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                var split = inputs[i].Split(',').ToList();

                int a = int.Parse(split[0].Split('-')[0]);
                int b = int.Parse(split[0].Split('-')[1]);
                int c = int.Parse(split[1].Split('-')[0]);
                int d = int.Parse(split[1].Split('-')[1]);

                if (c >= a && c <= b)
                {
                    sum++;
                }
                else if (d >= a && d <= b)
                {
                    sum++;
                }
                else if(a >= c && a <= d)
                {
                    sum++;
                }
                else if(b >= c && b <= d)
                {
                    sum++;
                }
            }

            return sum.ToString();
        }
    }
}

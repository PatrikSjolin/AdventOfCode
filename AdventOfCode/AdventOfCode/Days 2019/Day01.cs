using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            return System.IO.File.ReadAllLines(@"..\..\Data\2019\input01.txt").Select(x => (int.Parse(x) / 3) - 2).Sum().ToString();
        }

        public string RunTwo()
        {
            List<int> input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input01.txt").Select(x => (int.Parse(x))).ToList();

            int sum = 0;

            for (int i = 0; i < input.Count;)
            {
                input[i] = (input[i] / 3) - 2;

                if (input[i] <= 0)
                    i++;
                else
                    sum += input[i];
            }

            return sum.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day05 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            long[] inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input05.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();
            Computer c = new Computer();
            c.Input = 1;
            return c.Compute(inputs, 1).ToString();
        }

        private int GetValue(List<int> inputs, int v, int par)
        {
            if (par == 0)
                return inputs[inputs[v]];
            else
                return inputs[v];
        }

        public string RunTwo()
        {
            long[] inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input05.txt")[0].Split(',').Select(x => long.Parse(x)).ToArray();
            Computer c = new Computer();
            c.Input = 5;
            return c.Compute(inputs, 5).ToString();
        }
    }
}

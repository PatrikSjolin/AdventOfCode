using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day09 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input09.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            Computer c = new Computer(1);

            return c.Compute(inputs.ToArray()).ToString();
        }
        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input09.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            Computer c = new Computer(2);

            return c.Compute(inputs.ToArray()).ToString();
        }
    }
}

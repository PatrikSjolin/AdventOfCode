using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2021
{
    public class Day07 : IPuzzle
    {
        public bool Active => true;
        List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input07test.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            inputs = inputs.OrderBy();

            int fuelUsed = 0;


            double hej = inputs.Average();

            return "";
        }

        public string RunTwo()
        {
            return "";
        }
    }
}

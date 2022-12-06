using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day07 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input07.txt").ToList();

            return "";
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}

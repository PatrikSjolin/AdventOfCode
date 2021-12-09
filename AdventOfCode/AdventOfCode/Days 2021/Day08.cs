using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2021
{
    public class Day08 : IPuzzle
    {
        public bool Active => true;
        List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input08test.txt").ToList();

            for(int i = 0; i < inputs.Count; i++)
            {

            }

            return "";
        }

        public string RunTwo()
        {
            return "";
        }
    }
}

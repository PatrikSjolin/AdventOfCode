using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        private List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input01.txt").Select(x => int.Parse(x)).ToList();
            int larger = 0;
            int previous = int.MaxValue;
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] > previous)
                    larger++;
                previous = inputs[i];
            }

            return larger.ToString();
        }

        public string RunTwo()
        {
            int larger = 0;
            int previous = int.MaxValue;
            for (int i = 0; i < inputs.Count - 2; i++)
            {
                if (inputs[i] + inputs[i + 1] + inputs[i + 2] > previous)
                    larger++;
                previous = inputs[i] + inputs[i + 1] + inputs[i + 2];
            }

            return larger.ToString();
        }
    }
}

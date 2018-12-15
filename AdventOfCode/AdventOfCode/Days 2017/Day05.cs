using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day05 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input05.txt").ToList();
            int[] input = inputLines.Select(x => int.Parse(x)).ToArray();
            int index = 0;
            int totalJumps = 0;
            while (index < input.Length)
            {
                totalJumps++;
                int previousIndex = index;
                index = index + input[index];
                input[previousIndex]++;
            }

            return totalJumps.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input05.txt").ToList();
            int[] input = inputLines.Select(x => int.Parse(x)).ToArray();
            int index = 0;
            int totalJumps = 0;
            while (index < input.Length)
            {
                totalJumps++;
                int previousIndex = index;
                index = index + input[index];
                if (input[previousIndex] >= 3)
                {

                    input[previousIndex]--;
                }
                else
                {
                    input[previousIndex]++;
                }
            }

            return totalJumps.ToString();
        }
    }
}

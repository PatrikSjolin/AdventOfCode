using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class _5 : IPuzzle
    {
        public void RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input5.txt").ToList();
            int[] input = inputLines.Select(x => int.Parse(x)).ToArray();
            int index = 0;
            int totalJumps = 0;
            while(index < input.Length)
            {
                totalJumps++;
                int previousIndex = index;
                index = index + input[index];
                input[previousIndex]++;
            }

            Console.WriteLine(totalJumps);
        }

        public void RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input5.txt").ToList();
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

            Console.WriteLine(totalJumps);
        }
    }
}

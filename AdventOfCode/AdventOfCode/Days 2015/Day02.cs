using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2015
{
    public class Day02 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input02.txt").ToList();

            int sum = 0;

            for(int i = 0; i < inputLines.Count; i++)
            {
                List<string> split = inputLines[i].Split('x').ToList();

                int side1 = int.Parse(split[0]) * int.Parse(split[1]);
                int side2 = int.Parse(split[0]) * int.Parse(split[2]);
                int side3 = int.Parse(split[1]) * int.Parse(split[2]);

                sum += 2 * side1 + 2 * side2 + 2 * side3 + Math.Min(Math.Min(side1, side2), side3);
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input02.txt").ToList();

            int sum = 0;

            for (int i = 0; i < inputLines.Count; i++)
            {
                List<string> split = inputLines[i].Split('x').ToList();

                int side1 = int.Parse(split[0]);
                int side2 = int.Parse(split[1]);
                int side3 = int.Parse(split[2]);

                List<int> list = new List<int> { side1, side2, side3 };

                int short1 = list.Min();
                list.Remove(short1);
                int short2 = list.Min();


                sum += 2 * short1 + 2 * short2 + side1 * side2 * side3;
            }

            return sum.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day02 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input2.txt").ToList();

            int sum = 0;

            foreach (string s in inputLines)
            {
                List<int> numbers = s.Split('\t').Select(ss => int.Parse(ss)).ToList();
                sum += numbers.Max() - numbers.Min();
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input2.txt").ToList();

            int sum = 0;

            foreach (string s in inputLines)
            {
                List<string> numbers = s.Split('\t').ToList();
                List<int> myStringList = numbers.Select(ss => int.Parse(ss)).ToList();

                for (int i = 0; i < myStringList.Count; i++)
                {
                    for (int j = 0; j < myStringList.Count; j++)
                    {
                        if (j == i) continue;
                        if (myStringList[i] % myStringList[j] == 0)
                        {
                            sum += myStringList[i] / myStringList[j];
                        }
                    }
                }
            }

            return sum.ToString();
        }
    }
}

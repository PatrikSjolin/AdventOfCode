using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day06 : IPuzzle
    {
        private int indexOfEqual;
        HashSet<string> states = new HashSet<string>();
        List<string> listOfStates = new List<string>();

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input6.txt").ToList();
            List<int> split = inputLines[0].Split('\t').Select(x => int.Parse(x)).ToList();
            int size = split.Count;

            while (true)
            {
                string test = string.Join(".", split);

                if (states.Contains(test))
                {
                    indexOfEqual = listOfStates.IndexOf(test);
                    return states.Count.ToString();
                }

                states.Add(test);
                listOfStates.Add(test);

                int max = split.Max();

                int indexOfMax = split.IndexOf(max);

                split[indexOfMax] = 0;

                for (int i = indexOfMax + 1; i < (max + indexOfMax + 1); i++)
                {
                    split[i % size]++;
                }
            }
        }

        public string RunTwo()
        {
            return (states.Count - indexOfEqual).ToString();
        }
    }
}

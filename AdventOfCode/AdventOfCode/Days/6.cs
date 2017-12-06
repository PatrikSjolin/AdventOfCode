using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class _6 : IPuzzle
    {
        private int indexOfEqual;
        HashSet<string> states = new HashSet<string>();
        List<string> listOfStates = new List<string>();

        public void RunOne()
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
                    Console.WriteLine(states.Count);
                    return;
                }

                states.Add(test);
                listOfStates.Add(test);

                int max = split.Max();

                int indexOfMax = split.IndexOf(max);

                split[indexOfMax] = 0;

                for(int i = indexOfMax+1; i < (max+indexOfMax+1); i++)
                {
                    split[i % size]++;
                }                
            }
        }

        public void RunTwo()
        {
            Console.WriteLine(states.Count - indexOfEqual);
        }
    }
}

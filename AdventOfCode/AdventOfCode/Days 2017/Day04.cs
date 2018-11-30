using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day04 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input4.txt").ToList();

            int valid = 0;

            foreach (var line in inputLines)
            {
                Queue<string> queue = new Queue<string>(line.Split(' '));

                while (true)
                {
                    string test = queue.Dequeue();

                    if (queue.Count == 0)
                    {
                        valid++;
                        break;
                    }

                    if (queue.Contains(test))
                    {
                        break;
                    }
                }
            }
            return valid.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input4.txt").ToList();

            int valid = 0;

            foreach (var line in inputLines)
            {
                bool end = false;
                List<string> tests = line.Split(' ').ToList();
                for (int i = 0; i < tests.Count; i++)
                {
                    for (int j = 0; j < tests.Count; j++)
                    {
                        if (i == j) continue;
                        if (Utilities.IsAnagram(tests[i], tests[j]))
                        {
                            end = true;
                            break;
                        }
                    }

                    if (end) break;

                    if (i == tests.Count - 1)
                    {
                        valid++;
                    }
                }
            }
            return valid.ToString();
        }
    }
}

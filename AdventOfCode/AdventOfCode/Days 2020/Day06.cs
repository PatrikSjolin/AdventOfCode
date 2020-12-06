using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day06 : IPuzzle
    {
        public bool Active => true;

        List<List<string>> groups;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input06.txt").ToList();

            groups = new List<List<string>>();

            int count = 0;

            List<string> group = new List<string>();
            groups.Add(group);
            foreach (var input in inputs)
            {
                if (input == "")
                {
                    groups.Add(group = new List<string>());
                }
                else
                {
                    group.Add(input);
                }
            }

            List<char> letters = new List<char>();

            foreach (var g in groups)
            {
                foreach (var person in g)
                {
                    for (int i = 0; i < person.Length; i++)
                    {
                        if (!letters.Contains(person[i]))
                            letters.Add(person[i]);
                    }
                }
                count += letters.Count;
                letters.Clear();
            }

            return count.ToString();
        }

        public string RunTwo()
        {
            int count = 0;

            foreach (var g in groups)
            {
                for (int i = 0; i < g.Count - 1; i++)
                {
                    g[0] = new string(g[0].Intersect(g[i + 1]).ToArray());
                }
                count += g[0].Length;
            }

            return count.ToString();
        }
    }
}

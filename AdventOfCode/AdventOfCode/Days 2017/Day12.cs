using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day12 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input12.txt").ToList();

            Dictionary<int, List<int>> keys = new Dictionary<int, List<int>>();

            foreach (var input in inputLines)
            {
                string inp = input.Replace(" <-> ", ".");
                List<string> codes = inp.Split('.').ToList();
                List<int> values = codes[1].Split(',').Select(x => int.Parse(x)).ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    if (!keys.ContainsKey(int.Parse(codes[0])))
                    {
                        keys.Add(int.Parse(codes[0]), new List<int> { values[i] });
                    }
                    else
                    {
                        keys[int.Parse(codes[0])].Add(values[i]);
                    }
                }
            }

            int count = 0;
            List<int> programs = new List<int>();
            programs.Add(0);
            while (count != programs.Count)
            {
                count = programs.Count;

                foreach (var p in new List<int>(programs))
                {
                    foreach (var value in keys[p])
                    {
                        if (!programs.Contains(value))
                            programs.Add(value);
                    }
                }
            }
            return programs.Count.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input12.txt").ToList();

            Dictionary<int, List<int>> keys = new Dictionary<int, List<int>>();

            foreach (var input in inputLines)
            {
                string inp = input.Replace(" <-> ", ".");
                List<string> codes = inp.Split('.').ToList();
                List<int> values = codes[1].Split(',').Select(x => int.Parse(x)).ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    if (!keys.ContainsKey(int.Parse(codes[0])))
                    {
                        keys.Add(int.Parse(codes[0]), new List<int> { values[i] });
                    }
                    else
                    {
                        keys[int.Parse(codes[0])].Add(values[i]);
                    }
                }
            }

            List<string> groups = new List<string>();
            List<int> covered = new List<int>();
            foreach (var k in keys.Keys)
            {
                if (covered.Contains(k))
                    continue;
                int count = 0;
                List<int> programs = new List<int>();
                programs.Add(k);
                while (count != programs.Count)
                {
                    count = programs.Count;

                    foreach (var p in new List<int>(programs))
                    {
                        foreach (var value in keys[p])
                        {
                            if (!programs.Contains(value))
                                programs.Add(value);
                        }
                    }
                }
                string test = "";
                programs.Sort();
                for (int j = 0; j < programs.Count; j++)
                {
                    test += programs[j];
                    if (!covered.Contains(programs[j]))
                        covered.Add(programs[j]);
                }

                if (!groups.Contains(test))
                {
                    groups.Add(test);
                }
            }
            return groups.Count.ToString();
        }
    }
}

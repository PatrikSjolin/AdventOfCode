using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day06 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input06.txt").ToList();

            Dictionary<string, List<string>> orbits = new Dictionary<string, List<string>>();
            foreach (var pair in inputs)
            {
                List<string> pairs = pair.Split(')').ToList();

                if (orbits.ContainsKey(pairs[0]))
                {
                    orbits[pairs[0]].Add(pairs[1]);
                }
                else
                {
                    orbits.Add(pairs[0], new List<string> { pairs[1] });
                }
            }


            Traverse(orbits, orbits["COM"]);

            return sum.ToString();
        }
        int sum = 0;
        private int Traverse(Dictionary<string, List<string>> orbits, List<string> list)
        {
            int summ = 0;
            foreach (var o in list)
            {
                summ++;

                if (orbits.ContainsKey(o))
                {
                    summ += Traverse(orbits, orbits[o]);
                }

            }
            sum += summ;
            return summ;
        }

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input06.txt").ToList();

            Dictionary<string, string> orbits = new Dictionary<string, string>();
            foreach (var pair in inputs)
            {
                List<string> pairs = pair.Split(')').ToList();
                orbits.Add(pairs[1], pairs[0]);
            }

            List<string> path1 = GetPath(orbits, "YOU");
            List<string> path2 = GetPath(orbits, "SAN");

            for (int i = 0; i < path1.Count; i++)
            {
                if (path2.Contains(path1[i]))
                {
                    int start2 = path2.IndexOf(path1[i]);

                    return (i + start2 - 2).ToString();
                }
            }

            return "";
        }

        private List<string> GetPath(Dictionary<string, string> orbits, string node)
        {
            List<string> path = new List<string>();

            while (true)
            {
                path.Add(node);
                node = orbits[node];
                if (node == "COM")
                {
                    path.Add(node);
                    break;
                }
            }

            return path;
        }
    }
}

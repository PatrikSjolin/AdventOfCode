using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Component
    {
        public int PortOne { get; set; }
        public int PortTwo { get; set; }
        public List<Component> Children { get; set; }
    }

    public class Day24 : IPuzzle
    {
        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\input24.txt").ToList();

            List<Tuple<int, int>> tuples;
            Component root = new Component { PortOne = 0, PortTwo = 0 };

            tuples = new List<Tuple<int, int>>();

            foreach (var i in input)
            {
                var split = i.Split('/').ToList();
                tuples.Add(new Tuple<int, int>(int.Parse(split[0]), int.Parse(split[1])));
            }

            Populate(root, tuples, 0);

            CountSumPaths(root, 0, 0);

            int maxValue = paths.Select(x => x.Item2).Max();

            return maxValue.ToString();
        }

        List<Tuple<int, int>> paths = new List<Tuple<int, int>>();

        private void CountSumPaths(Component root, int sum, int pathCount)
        {
            sum += root.PortOne + root.PortTwo;
            pathCount++;

            if (root.Children == null)
            {
                paths.Add(new Tuple<int, int>(pathCount, sum));
            }
            else
            {
                foreach (var child in root.Children)
                {
                    CountSumPaths(child, sum, pathCount);
                }
            }
        }

        private void Populate(Component root, List<Tuple<int, int>> tuples, int matching)
        {
            List<Tuple<int, int>> matches = tuples.Where(x => x.Item1 == matching || x.Item2 == matching).ToList();

            if (matches.Count > 0)
            {
                root.Children = new List<Component>();
                foreach (var match in matches)
                {
                    int newMatch = match.Item1 == matching ? match.Item2 : match.Item1;

                    var component = new Component { PortOne = match.Item1, PortTwo = match.Item2 };
                    root.Children.Add(component);
                    List<Tuple<int, int>> newTuples = new List<Tuple<int, int>>(tuples);
                    newTuples.Remove(new Tuple<int, int>(match.Item1, match.Item2));
                    Populate(component, newTuples, newMatch);
                }
            }
        }

        public string RunTwo()
        {
            int maxPaths = paths.Select(x => x.Item1).Max();
            int maxValueForMaxPaths = paths.Where(x => x.Item1 == maxPaths).Select(x => x.Item2).Max();
            return maxValueForMaxPaths.ToString();
        }
    }
}

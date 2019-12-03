using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day03 : IPuzzle
    {
        public bool Active => true;

        Dictionary<string, Point> directionMap = new Dictionary<string, Point>
        {
            { "R", new Point(1, 0) },
            { "U", new Point(0, 1) },
            { "L", new Point(-1, 0) },
            { "D", new Point(0, -1) },
        };

        public string RunOne()
        {
            List<string> startInput = System.IO.File.ReadAllLines(@"..\..\Data\2019\input03.txt").ToList();
            List<string> wire1 = startInput[0].Split(',').ToList();
            List<string> wire2 = startInput[1].Split(',').ToList();

            HashSet<Point> points = new HashSet<Point>();

            int x = 0;
            int y = 0;

            foreach (var v in wire1)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                Point dir = directionMap[direction];

                for (int i = 0; i < moves; i++)
                {
                    x += dir.X;
                    y += dir.Y;
                    points.Add(new Point(x, y));
                }
            }

            x = 0;
            y = 0;

            int distance = int.MaxValue;

            foreach (var v in wire2)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                Point dir = directionMap[direction];

                for (int i = 0; i < moves; i++)
                {
                    x += dir.X;
                    y += dir.Y;

                    if (points.Contains(new Point(x, y)))
                    {
                        int manhattan = Math.Abs(x) + Math.Abs(y);
                        if (manhattan < distance)
                            distance = manhattan;
                    }
                }
            }

            return distance.ToString();
        }

        public string RunTwo()
        {
            List<string> startInput = System.IO.File.ReadAllLines(@"..\..\Data\2019\input03.txt").ToList();
            List<string> wire1 = startInput[0].Split(',').ToList();
            List<string> wire2 = startInput[1].Split(',').ToList();

            HashSet<Point> points = new HashSet<Point>();
            Dictionary<Point, int> pointSteps = new Dictionary<Point, int>();

            int x = 0;
            int y = 0;
            int steps = 0;

            foreach (var v in wire1)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));
                Point dir = directionMap[direction];
                for (int i = 0; i < moves; i++)
                {
                    steps++;
                    x += dir.X;
                    y += dir.Y;
                    Point p = new Point(x, y);
                    points.Add(p);
                    if (!pointSteps.ContainsKey(p))
                        pointSteps.Add(p, steps);
                }
            }

            x = 0;
            y = 0;
            steps = 0;

            int distance = int.MaxValue;

            foreach (var v in wire2)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));
                Point dir = directionMap[direction];
                for (int i = 0; i < moves; i++)
                {
                    steps++;
                    x += dir.X;
                    y += dir.Y;
                    if (points.Contains(new Point(x, y)))
                    {
                        int newDistance = steps + pointSteps[new Point(x, y)];
                        if (newDistance < distance)
                            distance = newDistance;
                    }
                }
            }

            return distance.ToString();
        }
    }
}

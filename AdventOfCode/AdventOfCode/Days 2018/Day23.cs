using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class NanoRobot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int Radius { get; set; }
    }

    public class Day23 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input23.txt").ToList();

            List<NanoRobot> robots = new List<NanoRobot>();

            foreach (var r in inputLines)
            {
                var split = r.Split('=');
                var splitNumbers = split[1].Split(',');
                int x = int.Parse(splitNumbers[0].Remove(0, 1));
                int y = int.Parse(splitNumbers[1]);
                int z = int.Parse(splitNumbers[2].Substring(0, splitNumbers[2].Length - 1));

                int radius = int.Parse(split[2]);

                robots.Add(new NanoRobot { X = x, Y = y, Z = z, Radius = radius });
            }

            var robot = robots.OrderByDescending(x => x.Radius).First();

            int inRange = 0;

            foreach (var r in robots)
            {
                int distance = Math.Abs(robot.X - r.X) + Math.Abs(robot.Y - r.Y) + Math.Abs(robot.Z - r.Z);

                if (distance <= robot.Radius)
                    inRange++;
            }

            return inRange.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input23.txt").ToList();

            List<NanoRobot> robots = new List<NanoRobot>();

            foreach (var r in inputLines)
            {
                var split = r.Split('=');
                var splitNumbers = split[1].Split(',');
                int x = int.Parse(splitNumbers[0].Remove(0, 1));
                int y = int.Parse(splitNumbers[1]);
                int z = int.Parse(splitNumbers[2].Substring(0, splitNumbers[2].Length - 1));

                int radius = int.Parse(split[2]);

                robots.Add(new NanoRobot { X = x, Y = y, Z = z, Radius = radius });
            }

            Dictionary<string, int> distances = new Dictionary<string, int>();

            foreach (var robot in robots)
            {
                int r = robot.Radius;

                for (int a = -r; a <= r; a++)
                {
                    for (int b = -r + Math.Abs(a); b <= r - Math.Abs(a); b++)
                    {
                        for (int c = -r + Math.Abs(a) + Math.Abs(b); c <= r - (Math.Abs(a) + Math.Abs(b)); c++)
                        {
                            string key = string.Format("{0},{1},{2}", robot.X + a, robot.Y + b, robot.Z + c);
                            if (distances.ContainsKey(key))
                            {
                                distances[key]++;
                            }
                            else
                            {
                                distances[key] = 1;
                            }
                        }
                    }
                }
            }

            distances = distances.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return "";
        }
    }
}

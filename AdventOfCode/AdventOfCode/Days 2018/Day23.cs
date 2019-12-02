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
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input23test2.txt").ToList();

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

            Point3D lower = new Point3D(int.MaxValue, int.MaxValue, int.MaxValue);
            Point3D upper = new Point3D(int.MinValue, int.MinValue, int.MinValue);

            foreach (var robot in robots)
            {
                if (robot.X - robot.Radius < lower.X)
                    lower.X = robot.X - robot.Radius;
                if (robot.Y - robot.Radius < lower.Y)
                    lower.Y = robot.Y - robot.Radius;
                if (robot.Z - robot.Radius < lower.Z)
                    lower.Z = robot.Z - robot.Radius;

                if (robot.X + robot.Radius > upper.X)
                    upper.X = robot.X + robot.Radius;
                if (robot.Y + robot.Radius > upper.Y)
                    upper.Y = robot.Y + robot.Radius;
                if (robot.Z + robot.Radius > upper.Z)
                    upper.Z = robot.Z + robot.Radius;
            }

            FindBox(lower, upper, robots);

            return "";
        }

        private List<Point3D> FindBox(Point3D lower, Point3D upper, List<NanoRobot> robots)
        {
            if(lower.Equals(upper))
            {
                return new List<Point3D> { lower };
            }

            List<KeyValuePair<Point3D, Point3D>> octals = GenerateOctals(lower, upper);

            Dictionary<KeyValuePair<Point3D, Point3D>, int> hitByRobots = HitByRobots(octals, robots);

            Dictionary<KeyValuePair<Point3D, Point3D>, int> sorted = hitByRobots.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            int highest = sorted.First().Value;

            List<Point3D> points = null;

            foreach(var lowestOctals in sorted.Where(x => x.Value == highest))
            {
                points = FindBox(lowestOctals.Key.Key, lowestOctals.Key.Value, robots);
            }

            return points;
        }

        private Dictionary<KeyValuePair<Point3D, Point3D>, int> HitByRobots(List<KeyValuePair<Point3D, Point3D>> octals, List<NanoRobot> robots)
        {
            Dictionary<KeyValuePair<Point3D, Point3D>, int> counts = new Dictionary<KeyValuePair<Point3D, Point3D>, int>();
            foreach(var o in octals)
            {
                counts[o] = 0;
                foreach(var r in robots)
                {
                    if(Math.Abs(r.X - o.Key.X) + Math.Abs(r.Y - o.Key.Y) + Math.Abs(r.Z - o.Key.Z) <= r.Radius ||
                       Math.Abs(r.X - o.Value.X) + Math.Abs(r.Y - o.Value.Y) + Math.Abs(r.Z - o.Value.Z) <= r.Radius)
                        
                    //if (r.X >= o.Key.X && r.Y >= o.Key.Y && r.Z >= o.Key.Z &&
                    //   r.X <= o.Value.X && r.Y <= o.Value.Y && r.Z <= o.Value.Z)
                        counts[o]++;
                }
            }
            return counts;
        }

        private List<KeyValuePair<Point3D, Point3D>> GenerateOctals(Point3D lower, Point3D upper)
        {
            int middleX = (upper.X + lower.X) / 2;
            int middleY = (upper.Y + lower.Y) / 2;
            int middleZ = (upper.Z + lower.Z) / 2;

            if(upper.X == lower.X)
            {
                return new List<KeyValuePair<Point3D, Point3D>> { new KeyValuePair<Point3D, Point3D>( lower, upper) };
            }

            return new List<KeyValuePair<Point3D, Point3D>>
            {
                new KeyValuePair<Point3D, Point3D>(new Point3D(lower.X, lower.Y, lower.Z), new Point3D(middleX, middleY, middleZ)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(middleX, lower.Y, lower.Z), new Point3D(upper.X, middleY, middleZ)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(lower.X, lower.Y, middleZ), new Point3D(middleX, middleY, upper.Z)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(middleX, lower.Y, middleZ), new Point3D(upper.X, middleY, upper.Z)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(lower.X, middleY, lower.Z), new Point3D(middleX, upper.Y, middleZ)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(middleX, middleY, lower.Z), new Point3D(upper.X, upper.Y, middleZ)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(lower.X, middleY, middleZ), new Point3D(middleX, upper.Y, upper.Z)),
                new KeyValuePair<Point3D, Point3D>(new Point3D(middleZ, middleY, middleZ), new Point3D(upper.X, upper.Y, upper.Z)),
            };
        }
    }
}

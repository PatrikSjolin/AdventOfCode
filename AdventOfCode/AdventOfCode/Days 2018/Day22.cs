using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day22 : IPuzzle
    {
        public bool Active => true;

        string[,] region;
        int depth = 0;
        Point target;
        Point start;

        int extra = 140;

        public string RunOne()
        {
            //depth = 510;
            //target = new Point(10, 10);

            depth = 3879;
            target = new Point(8, 713);

            start = new Point(0, 0);
            int sum = 0;

            region = new string[target.X + extra, target.Y + extra];

            for(int i = 0; i < target.X + extra; i++)
            {
                for(int j = 0; j < target.Y + extra; j++)
                {
                    region[i, j] = GetTypeForCoordinate(i, j, depth, target.X, target.Y);
                    if(i <= target.X && j <= target.Y)
                    {
                        if (region[i, j] == "=")
                            sum++;
                        else if (region[i, j] == "|")
                            sum += 2;
                    }
                }
            }


            //Console.WriteLine();
            //for (int i = 0; i <= target.Y; i++)
            //{
            //    for (int j = 0; j <= target.X; j++)
            //    {
            //        Console.Write(region[j, i]);
            //    }
            //    Console.WriteLine();
            //}

            return sum.ToString();
        }

        private int GetErosionLevel(int x, int y, int depth, int targetX, int targetY)
        {
            if (cache.ContainsKey(new Point(x, y)))
            {
                return cache[new Point(x, y)];
            }

            if ((x == 0 && y == 0) || (x == targetX && y == targetY))
            {
                int value = (0 + depth) % 20183;
                cache.Add(new Point(x, y), value);
                return value;
            }

            if (y == 0)
            {
                int value = (x * 16807 + depth) % 20183;
                cache.Add(new Point(x, y), value);
                return value;
            }

            if (x == 0)
            {
                int value = (y * 48271 + depth) % 20183;
                cache.Add(new Point(x, y), value);
                return value;
            }

            int erosionValue = ((GetErosionLevel(x - 1, y, depth, targetX, targetY) * GetErosionLevel(x, y - 1, depth, targetX, targetY)) + depth) % 20183;
            cache.Add(new Point(x, y), erosionValue);
            return erosionValue;
        }

        Dictionary<Point, int> cache = new Dictionary<Point, int>();

        private string GetTypeForCoordinate(int x, int y, int depth, int targetX, int targetY)
        {
            Dictionary<int, string> types = new Dictionary<int, string>
            {
                {0, "." }, //rocky
                {1, "=" }, //wet
                {2, "|" }  //narrow
            };

            int erosionLevel = GetErosionLevel(x, y, depth, targetX, targetY);
            return types[GetErosionLevel(x, y, depth, targetX, targetY) % 3];
        }

        public string RunTwo()
        {
            List<Point> unvisitedNodes = new List<Point>();
            int width = target.X + extra;
            int height = target.Y + extra;
            int[,] paths = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    unvisitedNodes.Add(new Point(i, j));
                    paths[i, j] = int.MaxValue;
                }
            }

            paths[0, 0] = 0;
            
            Thread T = new Thread(() => Utilities.FindShortestPath(region, unvisitedNodes, paths, new Point(0, 0), target, 1), 956000000);
            T.Start();

            while (T.IsAlive)
            {
            }

            Console.WriteLine();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (paths[j, i] < 1000)
                        Console.Write("0");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (paths[j, i] < 100)
                        Console.Write("0");
                    if (paths[j, i] < 10)
                        Console.Write("0");

                    if (j == target.X && i == target.Y)
                        Console.ForegroundColor = ConsoleColor.Red;

                    if (paths[j, i] == int.MaxValue)
                        Console.Write("XXXX");
                    else
                        Console.Write(paths[j, i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }

            int minutes = paths[target.X, target.Y];

            return minutes.ToString();
        }
    }
}

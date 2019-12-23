using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day20 : IPuzzle
    {
        public bool Active => false;

        Dictionary<string, List<Tuple<int, int>>> teleportations = new Dictionary<string, List<Tuple<int, int>>>();

        Dictionary<Tuple<int, int>, Tuple<int, int>> teleportPairs = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
        
        string[,] grid;

        int width = 0;
        int height = 0;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input20.txt").ToList();

            width = inputs[0].Length;
            height = inputs.Count;

            grid = new string[width, height];

            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    grid[i, j] = inputs[j][i].ToString();
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int ascii = char.Parse(grid[i, j]);
                    if (ascii > 64 && ascii < 91)
                    {
                        if (j + 1 < height)
                        {
                            int ascii2 = char.Parse(grid[i, j + 1]);

                            if (ascii2 > 64 && ascii2 < 91)
                            {
                                string s = ((char)ascii).ToString() + ((char)ascii2).ToString();
                                if (teleportations.ContainsKey(s))
                                {
                                    var otherPair = teleportations[s][0];

                                    if(j + 2 < height && grid[i, j + 2] != " ")
                                    {
                                        teleportations[s].Add(new Tuple<int, int>(i, j + 2));
                                        teleportPairs.Add(new Tuple<int, int>(i, j + 2), new Tuple<int, int>(otherPair.Item1, otherPair.Item2));
                                        teleportPairs.Add(new Tuple<int, int>(otherPair.Item1, otherPair.Item2), new Tuple<int, int>(i, j + 2));
                                    }
                                    else
                                    {
                                        teleportations[s].Add(new Tuple<int, int>(i, j - 1));
                                        teleportPairs.Add(new Tuple<int, int>(i, j - 1), new Tuple<int, int>(otherPair.Item1, otherPair.Item2));
                                        teleportPairs.Add(new Tuple<int, int>(otherPair.Item1, otherPair.Item2), new Tuple<int, int>(i, j - 1));
                                    }

                                }
                                else
                                {
                                    if (j + 2 < height && grid[i, j + 2] != " ")
                                    {
                                        teleportations.Add(s, new List<Tuple<int, int>> { new Tuple<int, int>(i, j + 2) });
                                    }
                                    else
                                    {
                                        teleportations.Add(s, new List<Tuple<int, int>> { new Tuple<int, int>(i, j - 1) });
                                    }
                                }
                            }
                        }
                        if (i + 1 < width)
                        {
                            int ascii2 = char.Parse(grid[i + 1, j]);

                            if (ascii2 > 64 && ascii2 < 91)
                            {
                                string s = ((char)ascii).ToString() + ((char)ascii2).ToString();
                                if (teleportations.ContainsKey(s))
                                {
                                    var otherPair = teleportations[s][0];
                                    if(i + 2 < width && grid[i + 2, j] != " ")
                                    {
                                        teleportations[s].Add(new Tuple<int, int>(i + 2, j));
                                        teleportPairs.Add(new Tuple<int, int>(i + 2, j), new Tuple<int, int>(otherPair.Item1, otherPair.Item2));
                                        teleportPairs.Add(new Tuple<int, int>(otherPair.Item1, otherPair.Item2), new Tuple<int, int>(i + 2, j));
                                    }
                                    else
                                    {
                                        teleportations[s].Add(new Tuple<int, int>(i - 1, j));
                                        teleportPairs.Add(new Tuple<int, int>(i - 1, j), new Tuple<int, int>(otherPair.Item1, otherPair.Item2));
                                        teleportPairs.Add(new Tuple<int, int>(otherPair.Item1, otherPair.Item2), new Tuple<int, int>(i - 1, j));
                                    }

                                }
                                else
                                {
                                    if(i + 2 < width && grid[i + 2, j] != " ")
                                    {
                                        teleportations.Add(s, new List<Tuple<int, int>> { new Tuple<int, int>(i + 2, j) });
                                    }
                                    else
                                    {
                                        teleportations.Add(s, new List<Tuple<int, int>> { new Tuple<int, int>(i - 1, j) });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Tuple<int, int> start = teleportations.First(x => x.Key == "AA").Value[0];
            Tuple<int, int> goal = teleportations.First(x => x.Key == "ZZ").Value[0];

            List<Point> unvisitedNodes = new List<Point>();
            int[,] paths = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j] == ".")
                    {
                        unvisitedNodes.Add(new Point(i, j));
                    }
                    paths[i, j] = int.MaxValue;
                }
            }

            paths[start.Item1, start.Item2] = 0;

            Thread t = new Thread(() => FindShortestPath(grid, unvisitedNodes, paths, start.Item1, start.Item2, goal.Item1, goal.Item2), 99999999);
            t.Start();
            while (t.IsAlive)
            {

            }
            
            int distance = paths[goal.Item1, goal.Item2];

            return distance.ToString();
        }

        private List<Point> GetNeighbours(string[,] map, int x, int y)
        {
            List<Point> neighbours = new List<Point>();

            var keyValueList = teleportPairs.Where(xx => xx.Key.Item1 == x && xx.Key.Item2 == y);

            if(keyValueList.Count() > 0)
            {
                var keyValue = keyValueList.First();
                neighbours.Add(new Point(keyValue.Value.Item1, keyValue.Value.Item2));
            }

            if (y + 1 < map.GetLength(1) && map[x, y + 1] == ".")
            {
                neighbours.Add(new Point(x, y + 1));
            }
            if (x + 1 < map.GetLength(0) && map[x + 1, y] == ".")
            {
                neighbours.Add(new Point(x + 1, y));
            }
            if (y - 1 >= 0 && map[x, y - 1] == ".")
            {
                neighbours.Add(new Point(x, y - 1));
            }
            if (x - 1 >= 0 && map[x - 1, y] == ".")
            {
                neighbours.Add(new Point(x - 1, y));
            }

            return neighbours;
        }

        private void FindShortestPath(string[,] map, List<Point> unvisitedNodes, int[,] paths, int x, int y, int destinationX, int destinationY)
        {
            List<Point> neighbours = GetNeighbours(map, x, y);

            unvisitedNodes.Remove(new Point(x, y));

            foreach (var n in neighbours)
            {
                int nx = n.X;
                int ny = n.Y;

                if (paths[x, y] + 1 < paths[nx, ny])
                {
                    paths[nx, ny] = paths[x, y] + 1;

                }
            }

            List<Point> notVisitedNeighbours = unvisitedNodes.Where(xx => paths[xx.X, xx.Y] < int.MaxValue).ToList();

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in notVisitedNeighbours)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (smallestX == destinationX && smallestY == destinationY)
                return;

            if (notVisitedNeighbours.Count() > 0)
                FindShortestPath(map, unvisitedNodes, paths, smallestX, smallestY, destinationX, destinationY);
        }

        public string RunTwo()
        {

            Tuple<int, int> start = teleportations.First(x => x.Key == "AA").Value[0];
            Tuple<int, int> goal = teleportations.First(x => x.Key == "ZZ").Value[0];

            List<Point> unvisitedNodes = new List<Point>();
            int[,] paths = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j] == ".")
                    {
                        unvisitedNodes.Add(new Point(i, j));
                    }
                    paths[i, j] = int.MaxValue;
                }
            }

            paths[start.Item1, start.Item2] = 0;

            Thread t = new Thread(() => FindShortestPath(grid, unvisitedNodes, paths, start.Item1, start.Item2, goal.Item1, goal.Item2), 99999999);
            t.Start();
            while (t.IsAlive)
            {

            }

            int distance = paths[goal.Item1, goal.Item2];

            return distance.ToString();
        }
    }
}

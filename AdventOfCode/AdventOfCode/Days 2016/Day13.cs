using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day13 : IPuzzle
    {
        private int taskNumber = 13;

        public bool Active => false;

        public void GoOne()
        {
            Console.BufferWidth = 1000;
            Console.BufferHeight = 1000;
            Console.SetWindowSize(426 / 2, 114 / 2);
            int numberOfSteps = CalcSteps();
            //Console.WriteLine(numberOfSteps);
        }

        private bool IsWall(int x, int y, int favNumber)
        {
            int number = x * x + 3 * x + 2 * x * y + y + y * y;
            number += favNumber;

            string binary = Convert.ToString(number, 2);

            int sum = 0;
            foreach (var c in binary)
            {
                if (c == '1')
                {
                    sum++;
                }
            }

            return sum % 2 == 1;
        }

        private int CalcSteps()
        {
            int favNumber = 1364;
            int columns = 50;
            int rows = 50;

            //int favNumber = 10;
            //int columns = 10;
            //int rows = 7;

            bool[,] map = new bool[rows, columns];

            map = ConstructMap(map, favNumber);

            List<Tuple<int, int>> unvisitedNodes = new List<Tuple<int, int>>();
            int[,] paths = new int[rows, columns];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == false)
                    {
                        paths[i, j] = int.MaxValue;
                        unvisitedNodes.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            paths[1, 1] = 0;

            FindShortestPath(map, unvisitedNodes, paths, 1, 1);

            DrawMap(map, paths);

            Console.WriteLine(paths[39, 31]);

            int sum = 0;
            
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (paths[i, j] <= 50 && paths[i, j] > 0)
                    {
                        sum++;
                    }
                }
            }

            Console.WriteLine(sum + 1);

            return paths[39, 31];
        }

        private void FindShortestPath(bool[,] map, List<Tuple<int, int>> unvisitedNodes, int[,] paths, int x, int y)
        {
            List<Tuple<int, int>> neighbours = GetNeighbours(map, x, y);

            unvisitedNodes.Remove(new Tuple<int, int>(x, y));

            foreach (var n in neighbours)
            {
                int nx = n.Item1;
                int ny = n.Item2;

                if (paths[x, y] + 1 < paths[nx, ny])
                {
                    paths[nx, ny] = paths[x, y] + 1;
                }
            }

            foreach (var n in neighbours)
            {
                int nx = n.Item1;
                int ny = n.Item2;

                if (unvisitedNodes.Contains(new Tuple<int, int>(nx, ny)))
                {
                    FindShortestPath(map, unvisitedNodes, paths, nx, ny);
                }
            }
        }

        private List<Tuple<int, int>> GetNeighbours(bool[,] map, int x, int y)
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

            if (y + 1 < map.GetLength(1) && map[x, y + 1] == false)
            {
                neighbours.Add(new Tuple<int, int>(x, y + 1));
            }
            if (x + 1 < map.GetLength(0) && map[x + 1, y] == false)
            {
                neighbours.Add(new Tuple<int, int>(x + 1, y));
            }
            if (y - 1 >= 0 && map[x, y - 1] == false)
            {
                neighbours.Add(new Tuple<int, int>(x, y - 1));
            }
            if (x - 1 >= 0 && map[x - 1, y] == false)
            {
                neighbours.Add(new Tuple<int, int>(x - 1, y));
            }

            return neighbours;
        }

        private void DrawMap(bool[,] map, int[,] paths)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(i == 39 && j == 31)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if(map[i, j])
                    {
                        Console.Write("###");
                    }
                    else
                    {
                        if(paths[i, j] == int.MaxValue)
                        {
                            Console.Write("...");
                        }
                        else
                        {
                            int size = paths[i, j].ToString().Length;
                            if(size == 1)
                            {
                                Console.Write("00" + paths[i, j]);
                            }
                            else if(size == 2)
                            {
                                Console.Write("0" + paths[i, j]);
                            }
                            else
                            {
                                Console.Write(paths[i, j]);
                            }
                        }                        
                    }
                }
                Console.WriteLine();
            }
        }

        private bool[,] ConstructMap(bool[,] map, int favNumber)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = IsWall(j, i, favNumber);
                }
            }

            return map;
        }

        public void GoTwo()
        {
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}

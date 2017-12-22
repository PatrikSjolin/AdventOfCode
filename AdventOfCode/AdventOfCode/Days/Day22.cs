using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day22 : IPuzzle
    {
        public string RunOne()
        {
            List<string> rules = System.IO.File.ReadAllLines(@"..\..\input22.txt").ToList();

            Tuple<int, int> direction = new Tuple<int, int>(0, 1);

            Tuple<int, int> north = new Tuple<int, int>(0, -1);
            Tuple<int, int> east = new Tuple<int, int>(1, 0);
            Tuple<int, int> south = new Tuple<int, int>(0, 1);
            Tuple<int, int> west = new Tuple<int, int>(-1, 0);

            List<Tuple<int, int>> directions = new List<Tuple<int, int>> { north, east, south, west };
            int curDir = 3;

            int gridSize = 256;

            string[,] grid = new string[gridSize, gridSize];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = ".";
                }
            }

            int x = gridSize / 2;
            int y = gridSize / 2;

            int size = rules.Count;


            for (int i = 0; i < rules.Count; i++)
            {
                for (int j = 0; j < rules[i].Length; j++)
                {
                    grid[i + (x - (size / 2)), j + (y - (size / 2))] = rules[i][j].ToString();
                }
            }

            int infected = 0;

            for (int i = 0; i < 10000; i++)
            {
                if (grid[x, y] == ".")
                {
                    infected++;
                    curDir = (curDir + 1) % 4;
                    direction = directions[curDir];

                    grid[x, y] = "#";

                    x += direction.Item1;
                    y += direction.Item2;
                }
                else
                {
                    if (curDir == 0)
                        curDir = 3;
                    else
                        curDir--;

                    direction = directions[curDir];

                    grid[x, y] = ".";

                    x += direction.Item1;
                    y += direction.Item2;
                }
            }

            return infected.ToString();
        }

        public string RunTwo()
        {
            List<string> rules = System.IO.File.ReadAllLines(@"..\..\input22.txt").ToList();

            Tuple<int, int> direction = new Tuple<int, int>(0, 1);

            Tuple<int, int> north = new Tuple<int, int>(0, -1);
            Tuple<int, int> east = new Tuple<int, int>(1, 0);
            Tuple<int, int> south = new Tuple<int, int>(0, 1);
            Tuple<int, int> west = new Tuple<int, int>(-1, 0);

            List<Tuple<int, int>> directions = new List<Tuple<int, int>> { north, east, south, west };
            int curDir = 3;

            int gridSize = 400;

            string[,] grid = new string[gridSize, gridSize];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = ".";
                }
            }

            int x = gridSize / 2;
            int y = gridSize / 2;

            int size = rules.Count;


            for (int i = 0; i < rules.Count; i++)
            {
                for (int j = 0; j < rules[i].Length; j++)
                {
                    grid[i + (x - (size / 2)), j + (y - (size / 2))] = rules[i][j].ToString();
                }
            }

            int infected = 0;

            for (int i = 0; i < 10000000; i++)
            {
                if (grid[x, y] == ".")
                {
                    curDir = (curDir + 1) & 3;
                    direction = directions[curDir];

                    grid[x, y] = "W";

                    x += direction.Item1;
                    y += direction.Item2;
                }
                else if (grid[x, y] == "W")
                {
                    infected++;
                    grid[x, y] = "#";

                    x += direction.Item1;
                    y += direction.Item2;
                }
                else if (grid[x, y] == "#")
                {
                    curDir = curDir == 0 ? 3 : curDir - 1;
                    direction = directions[curDir];

                    grid[x, y] = "F";

                    x += direction.Item1;
                    y += direction.Item2;
                }
                else
                {
                    curDir = (curDir + 2) & 3;
                    direction = directions[curDir];

                    grid[x, y] = ".";

                    x += direction.Item1;
                    y += direction.Item2;
                }
            }

            return infected.ToString();
        }

        private void PaintGrid(string[,] grid, int x, int y)
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(grid[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day22 : IPuzzle
    {
        private Tuple<int, int> north = new Tuple<int, int>(0, -1);
        private Tuple<int, int> east = new Tuple<int, int>(1, 0);
        private Tuple<int, int> south = new Tuple<int, int>(0, 1);
        private Tuple<int, int> west = new Tuple<int, int>(-1, 0);

        private List<Tuple<int, int>> directions;

        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2017\input22.txt").ToList();
            directions = new List<Tuple<int, int>> { north, east, south, west };
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
            int size = input.Count;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i + (x - (size / 2)), j + (y - (size / 2))] = input[i][j].ToString();
                }
            }

            int infected = 0;
            int currentDirection = 3;

            for (int i = 0; i < 10000; i++)
            {
                if (grid[x, y] == ".")
                {
                    infected++;
                    currentDirection = (currentDirection + 1) % 4;
                    grid[x, y] = "#";
                }
                else
                {
                    currentDirection = currentDirection == 0 ? 3 : currentDirection - 1;
                    grid[x, y] = ".";
                }
                x += directions[currentDirection].Item1;
                y += directions[currentDirection].Item2;
            }

            return infected.ToString();
        }

        public string RunTwo()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2017\input22.txt").ToList();
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
            int size = input.Count;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i + (x - (size / 2)), j + (y - (size / 2))] = input[i][j].ToString();
                }
            }

            int infected = 0;
            int currentDirection = 3;

            for (int i = 0; i < 10000000; i++)
            {
                if (grid[x, y] == ".")
                {
                    currentDirection = (currentDirection + 1) & 3;
                    grid[x, y] = "W";
                }
                else if (grid[x, y] == "W")
                {
                    infected++;
                    grid[x, y] = "#";
                }
                else if (grid[x, y] == "#")
                {
                    currentDirection = currentDirection == 0 ? 3 : currentDirection - 1;
                    grid[x, y] = "F";
                }
                else
                {
                    currentDirection = (currentDirection + 2) & 3;
                    grid[x, y] = ".";
                }
                x += directions[currentDirection].Item1;
                y += directions[currentDirection].Item2;
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

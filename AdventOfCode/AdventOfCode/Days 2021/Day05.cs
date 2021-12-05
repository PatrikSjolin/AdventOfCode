using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    public class Day05 : IPuzzle
    {
        public bool Active => true;
        List<string> inputs;

        int gridSize = 1000;
        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input05.txt").ToList();

            int[,] grid = new int[gridSize, gridSize];
            foreach (var line in inputs)
            {
                var points = line.Split('-');
                points[0] = points[0].Replace(" ", "");
                points[1] = points[1].Replace(" ", "");
                points[1] = points[1].Replace(">", "");

                int x1 = int.Parse(points[0].Split(',')[0]);
                int y1 = int.Parse(points[0].Split(',')[1]);
                int x2 = int.Parse(points[1].Split(',')[0]);
                int y2 = int.Parse(points[1].Split(',')[1]);

                if (y2 - y1 == 0)
                {
                    if (x2 > x1)
                    {
                        for (int i = x1; i <= x2; i++)
                            grid[y2, i] += 1;
                    }
                    else
                    {
                        for (int i = x2; i <= x1; i++)
                            grid[y2, i] += 1;
                    }
                }
                if (x2 - x1 == 0)
                {
                    if (y2 > y1)
                    {
                        for (int i = y1; i <= y2; i++)
                            grid[i, x2] += 1;
                    }
                    else
                    {
                        for (int i = y2; i <= y1; i++)
                            grid[i, x2] += 1;
                    }
                }

            }

            int overlaps = 0;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] > 1)
                        overlaps++;
                }
            }
            return overlaps.ToString();
        }

        private void PrintGrid(int[,] grid)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        public string RunTwo()
        {
            int[,] grid = new int[gridSize, gridSize];
            foreach (var line in inputs)
            {
                var points = line.Split('-');
                points[0] = points[0].Replace(" ", "");
                points[1] = points[1].Replace(" ", "");
                points[1] = points[1].Replace(">", "");

                int x1 = int.Parse(points[0].Split(',')[0]);
                int y1 = int.Parse(points[0].Split(',')[1]);
                int x2 = int.Parse(points[1].Split(',')[0]);
                int y2 = int.Parse(points[1].Split(',')[1]);

                if (y2 - y1 == 0)
                {
                    if (x2 > x1)
                    {
                        for (int i = x1; i <= x2; i++)
                            grid[y2, i] += 1;
                    }
                    else
                    {
                        for (int i = x2; i <= x1; i++)
                            grid[y2, i] += 1;
                    }
                }
                else if (x2 - x1 == 0)
                {
                    if (y2 > y1)
                    {
                        for (int i = y1; i <= y2; i++)
                            grid[i, x2] += 1;
                    }
                    else
                    {
                        for (int i = y2; i <= y1; i++)
                            grid[i, x2] += 1;
                    }
                }
                else
                {
                    if (x1 > x2)
                    {
                        int tmpX = x2;
                        int tmpY = y2;
                        x2 = x1;
                        y2 = y1;
                        x1 = tmpX;
                        y1 = tmpY;
                    }
                    int index = 0;
                    for (int i = x1; i <= x2; i++)
                    {
                        grid[y1 + index, i] += 1;
                        if (y1 > y2)
                            index--;
                        else
                            index++;
                    }

                }
            }

            int overlaps = 0;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] > 1)
                        overlaps++;
                }
            }
            return overlaps.ToString();
        }
    }
}

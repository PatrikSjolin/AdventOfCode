using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdventOfCode.Days_2019
{
    public class Day15 : IPuzzle
    {
        public bool Active => false;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input15.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            Computer c = new Computer(0);
            grid[x, y] = "D";

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            c = new Computer(0);

            c.OutputEvent += C_OutputEvent;
            c.InputEvent += C_InputEvent;
            distance[size / 2, size / 2] = 0;
            if (visualize)
            {
                Console.Clear();

                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    for (int j = 0; j < grid.GetLength(0); j++)
                    {
                        if (grid[j, i] == null)
                            Console.Write(" ");
                        else
                            Console.Write(grid[j, i]);
                    }
                    Console.WriteLine();
                }
            }
            c.Compute(inputs.ToArray());

            return distance[goalY, goalX].ToString();
        }

        static int size = 44;


        int[,] distance = new int[size, size];
        string[,] grid = new string[size, size];

        int x = size / 2;
        int y = size / 2;

        Dictionary<int, int> directions = new Dictionary<int, int>
        {
            { 0, 4 }, //East
            { 1, 1 }, //North
            { 2, 3 }, //West
            { 3, 2 }, //South
        };

        int rotation = 0;

        int oldX = size / 2;
        int oldY = size / 2;

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            if (!play)
            {
                if (x == size / 2 && y == size / 2 && c.Output == 1)
                {
                    c.Input = 0;
                    return;
                }

                if (x - oldX < 0) //Moved west
                {
                    rotation = 1; //Try north
                    c.Input = rotation;
                    rotation++;
                }
                else if (x - oldX > 0) //Moved east
                {
                    rotation = 3;
                    c.Input = 2;
                    rotation++;
                }
                else if (y - oldY < 0) //Moved north
                {
                    rotation = 0;
                    c.Input = 4;
                    rotation++;
                }
                else if (y - oldY > 0) //Moved south
                {
                    rotation = 2;
                    c.Input = 3;
                    rotation++;
                }
                else
                {
                    if (rotation == 4)
                        rotation = 0;
                    if (rotation == -1)
                        rotation = 3;

                    c.Input = directions[rotation];
                    rotation++;
                }
                oldY = y;
                oldX = x;
            }
            else
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    c.Input = 3;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    c.Input = 4;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    c.Input = 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    c.Input = 2;
                }
            }
        }

        bool play = false;
        bool visualize = false;

        private void C_OutputEvent(object sender, EventArgs e)
        {
            if (visualize)
                Thread.Sleep(5);

            Computer c = ((Computer)sender);

            long output = c.Output;

            if (output == 0)
            {
                if (c.Input == 1)
                {
                    grid[y - 1, x] = "#";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        Console.Write("#");
                    }
                }
                else if (c.Input == 2)
                {
                    grid[y + 1, x] = "#";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        Console.Write("#");
                    }
                }
                else if (c.Input == 3)
                {
                    grid[y, x - 1] = "#";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write("#");
                    }
                }
                else if (c.Input == 4)
                {
                    grid[y, x + 1] = "#";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        Console.Write("#");
                    }
                }
            }
            else if (output == 1)
            {
                if (grid[y, x] != "V")
                    grid[y, x] = ".";

                if (grid[y, x] != "V")
                {
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(".");
                    }
                }

                if (c.Input == 1)
                {
                    grid[y - 1, x] = "D";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        Console.Write("D");
                    }

                    if (distance[y - 1, x] == 0)
                        distance[y - 1, x] = distance[y, x] + 1;

                    y -= 1;
                }
                else if (c.Input == 2)
                {
                    grid[y + 1, x] = "D";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        Console.Write("D");
                    }
                    if (distance[y + 1, x] == 0)
                        distance[y + 1, x] = distance[y, x] + 1;
                    y += 1;
                }
                else if (c.Input == 3)
                {
                    grid[y, x - 1] = "D";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write("D");
                    }
                    if (distance[y, x - 1] == 0)
                        distance[y, x - 1] = distance[y, x] + 1;
                    x -= 1;
                }
                else if (c.Input == 4)
                {
                    grid[y, x + 1] = "D";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        Console.Write("D");
                    }
                    if (distance[y, x + 1] == 0)
                        distance[y, x + 1] = distance[y, x] + 1;
                    x += 1;
                }
            }
            else if (output == 2)
            {
                grid[y, x] = ".";
                if (visualize)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(".");
                }

                if (c.Input == 1)
                {
                    goalX = x;
                    goalY = y - 1;
                    grid[y - 1, x] = "V";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        Console.Write("V");
                    }

                    if (distance[y - 1, x] == 0)
                        distance[y - 1, x] = distance[y, x] + 1;

                    y -= 1;
                }
                else if (c.Input == 2)
                {
                    goalX = x;
                    goalY = y + 1;
                    grid[y + 1, x] = "V";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        Console.Write("V");
                    }
                    if (distance[y + 1, x] == 0)
                        distance[y + 1, x] = distance[y, x] + 1;
                    y += 1;
                }
                else if (c.Input == 3)
                {
                    goalX = x - 1;
                    goalY = y;
                    grid[y, x - 1] = "V";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write("V");
                    }
                    if (distance[y, x - 1] == 0)
                        distance[y, x - 1] = distance[y, x] + 1;
                    x -= 1;
                }
                else if (c.Input == 4)
                {
                    goalX = x + 1;
                    goalY = y;
                    grid[y, x + 1] = "V";
                    if (visualize)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        Console.Write("V");
                    }
                    if (distance[y, x + 1] == 0)
                        distance[y, x + 1] = distance[y, x] + 1;
                    x += 1;
                }
            }
            if (visualize)
                Console.SetCursorPosition(0, 30);
        }

        int goalX = 0;
        int goalY = 0;

        public string RunTwo()
        {
            if (visualize)
            {
                Console.Clear();

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == null)
                            Console.Write(" ");
                        else
                            Console.Write(grid[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            bool[,] map = new bool[size, size];

            int[,] paths = new int[size, size];

            List<Point> unvisitedNodes = new List<Point>();


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == ".")
                    {
                        unvisitedNodes.Add(new Point(i, j));
                        map[i, j] = true;
                    }
                    else
                    {
                        map[i, j] = false;
                    }
                    paths[i, j] = int.MaxValue;
                }
            }

            paths[goalY, goalX] = 0;
            Utilities.FindShortestPath(map, unvisitedNodes, paths, goalY, goalX);

            int maxOxyg = int.MinValue;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (paths[i, j] != int.MaxValue && paths[i, j] > maxOxyg)
                        maxOxyg = paths[i, j];
                }
            }

            return maxOxyg.ToString();
        }
    }
}

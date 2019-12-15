using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdventOfCode.Days_2019
{
    public class Day13 : IPuzzle
    {
        public bool Active => true;

        Dictionary<Tuple<long, long>, long> gameBoard = new Dictionary<Tuple<long, long>, long>();

        string[,] grid;

        int i = 0;

        Computer c = new Computer(0);

        long[] inputArray;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            inputArray = inputs.ToArray();

            c.OutputEvent += C_OutputEvent;
            c.Compute(inputArray);

            int sum = 0;

            foreach (var tile in gameBoard)
            {
                if (tile.Value == 2)
                    sum++;
            }

            int twos = gameBoard.Count(x => x.Value == 2);

            return sum.ToString();
        }

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            long output = c.Output;

            if (i % 3 == 2)
            {
                gameBoard.Add(new Tuple<long, long>(x, y), output);
            }
            else if (i % 3 == 1)
            {
                y = output;
            }
            else
            {
                x = output;
            }
            i++;
        }

        long x = 0;
        long y = 0;

        long points = 0;
        bool score = false;
        int scoreCount = 0;

        private void C_OutputEvent2(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            long output = c.Output;

            if (output == -1 || score)
            {
                score = true;
                scoreCount++;

                if (scoreCount == 3)
                {
                    points = output;
                    score = false;
                    scoreCount = 0;
                }
            }
            else
            {
                if (i % 3 == 2)
                {
                    if (gameBoard.ContainsKey(new Tuple<long, long>(x, y)))
                        gameBoard[new Tuple<long, long>(x, y)] = output;
                    else
                        gameBoard.Add(new Tuple<long, long>(x, y), output);
                }
                else if (i % 3 == 1)
                {
                    y = output;
                }
                else
                {
                    x = output;
                }
                i++;
            }
        }

        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
            gameBoard.Clear();

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            inputArray = inputs.ToArray();

            i = 0;
            inputArray[0] = 2;

            c = new Computer(0);

            c.OutputEvent += C_OutputEvent2;
            c.InputEvent += C_InputEvent;

            c.Compute(inputArray);

            return points.ToString();
        }

        bool visualize = false;
        bool play = false;

        bool first = true;
        string[,] lastGrid;

        private void C_InputEvent(object sender, EventArgs e)
        {
            if(!play && visualize)
            {
                Thread.Sleep(40);
            }
            if (visualize)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("SCORE: " + points);
                InitBoard();
                if (first)
                {
                    Console.Clear();
                    Console.Write("SCORE: " + points);
                    PrintGrid();
                    first = false;
                }
                else
                {
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < grid.GetLength(0); j++)
                        {
                            if (grid[j, i] != lastGrid[j, i])
                            {
                                Console.SetCursorPosition(j, i + 1);

                                string letter = "";

                                if (grid[j, i] == "4")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    letter = "o";
                                }
                                if (grid[j, i] == "3")
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    letter = "¯";
                                }
                                if (grid[j, i] == "2")
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    letter = "O";
                                }
                                if (grid[j, i] == "1")
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    letter = "\u2588";
                                }
                                if(grid[j,i] == "0")
                                {
                                    letter = " ";
                                }

                                Console.Write(letter);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(0, 25);
                            }
                        }
                    }
                }

                lastGrid = new string[grid.GetLength(0), grid.GetLength(1)];
                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    for (int j = 0; j < grid.GetLength(0); j++)
                    {
                        lastGrid[j, i] = grid[j, i];
                    }
                }
            }

            Computer c = ((Computer)sender);


            if (!play)
            {
                Tuple<long, long> ball = gameBoard.Where(x => x.Value == 4).FirstOrDefault().Key;
                Tuple<long, long> paddle = gameBoard.Where(x => x.Value == 3).FirstOrDefault().Key;

                if (paddle.Item1 < ball.Item1)
                    c.Input = 1;
                else if (paddle.Item1 > ball.Item1)
                    c.Input = -1;
                else
                    c.Input = 0;

            }
            else
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    c.Input = -1;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    c.Input = 1;
                }
                else
                {
                    c.Input = 0;
                }
            }
        }

        private void PrintGrid()
        {
            Console.WriteLine();
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {

                    string letter = "";

                    if (grid[j, i] == "4")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        letter = "o";
                    }
                    if (grid[j, i] == "3")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        letter = "¯";
                    }
                    if (grid[j, i] == "2")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        letter = "O";
                    }
                    if (grid[j, i] == "1")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        letter = "\u2588";
                    }
                    if (grid[j, i] == "0")
                    {
                        letter = " ";
                    }

                    Console.Write(letter);
                    //Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        private void InitBoard()
        {
            long minX = int.MaxValue;
            long minY = int.MaxValue;
            long maxX = int.MinValue;
            long maxY = int.MinValue;

            foreach (var b in gameBoard)
            {
                if (b.Key.Item1 < minX)
                    minX = b.Key.Item1;

                if (b.Key.Item1 > maxX)
                    maxX = b.Key.Item1;

                if (b.Key.Item2 < minY)
                    minY = b.Key.Item2;

                if (b.Key.Item2 > maxY)
                    maxY = b.Key.Item2;
            }

            grid = new string[maxX + 1, maxY + 1];

            foreach (var b in gameBoard)
            {
                grid[b.Key.Item1, b.Key.Item2] = b.Value.ToString();
            }


            if (lastGrid == null)
            {
                lastGrid = new string[grid.GetLength(0), grid.GetLength(1)];
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        lastGrid[i, j] = "=";
                    }
                }
            }
        }
    }
}

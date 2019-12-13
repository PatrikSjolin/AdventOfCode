using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day13 : IPuzzle
    {
        public bool Active => false;

        Dictionary<Tuple<long, long>, long> gameBoard = new Dictionary<Tuple<long, long>, long>();

        string[,] grid;

        int i = 0;

        Computer c = new Computer(0);
        List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

        long[] inputArray;

        public string RunOne()
        {
            //List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            inputArray = inputs.ToArray();

            c.OutputEvent += C_OutputEvent;
            c.Compute(inputArray);

            int sum = 0;

            foreach(var tile in gameBoard)
            {
                if (tile.Value == 2)
                    sum++;
            }

            int twos = gameBoard.Count(x => x.Value == 2);

            return sum.ToString();
        }

        long x = 0;
        long y = 0;

        bool score = false;
        int scoreCount = 0;


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

        long points = 0;
        bool play = false;

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

                if (i > 3168)
                {
                }
            }
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
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

        bool visualize = true;


        Dictionary<Tuple<long, long>, long> lastGameBoard = new Dictionary<Tuple<long, long>, long>();


        string[,] lastGrid;

        private void C_InputEvent(object sender, EventArgs e)
        {
            if (play || visualize)
            {
                Console.Clear();
                Console.Write("SCORE: \n" + points);
                InitBoard();
                PrintGrid();

                //    for (int i = 0; i < grid.GetLength(0); i++)
                //    {
                //        for (int j = 0; j < grid.GetLength(1); j++)
                //        {
                //            if(grid[i, j] != lastGrid[i, j])
                //            {
                //                Console.SetCursorPosition(i, j);


                //                if (grid[i, j] == "4")
                //                {
                //                    Console.ForegroundColor = ConsoleColor.Red;
                //                }
                //                if (grid[i, j] == "3")
                //                {
                //                    Console.ForegroundColor = ConsoleColor.Green;
                //                }
                //                if (grid[i, j] == "2")
                //                {
                //                    Console.ForegroundColor = ConsoleColor.Blue;
                //                }
                //                if (grid[i, j] == "1")
                //                {
                //                    Console.ForegroundColor = ConsoleColor.Gray;
                //                }


                //                Console.Write(grid[i, j]);
                //            }
                //        }
                //    }

                //    lastGrid = new string[grid.GetLength(0), grid.GetLength(1)];
                //    for(int i = 0; i < grid.GetLength(0); i++)
                //    {
                //        for(int j = 0; j < grid.GetLength(1); j++)
                //        {
                //            lastGrid[i, j] = grid[i, j];
                //        }
                //    }
            }

            Computer c = ((Computer)sender);

            Tuple<long, long> ball = gameBoard.Where(x => x.Value == 4).FirstOrDefault().Key;
            Tuple<long, long> paddle = gameBoard.Where(x => x.Value == 3).FirstOrDefault().Key;

            if (!play)
            {

            if (paddle.Item1 < ball.Item1)
            {
                c.Input = 1;
            }
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
                for(int j = 0; j < grid.GetLength(0); j++)
                {
                    if(grid[j, i] == "4")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if(grid[j,i] == "3")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (grid[j, i] == "2")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if (grid[j, i] == "1")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(grid[j, i]);
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

            foreach(var b in gameBoard)
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

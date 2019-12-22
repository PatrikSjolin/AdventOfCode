using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day19 : IPuzzle
    {
        public bool Active => false;

        static int size = 2000;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input19.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    x = i;
                    y = j;
                    Computer c = new Computer(0);
                    c.OutputEvent += C_OutputEvent;
                    c.InputEvent += C_InputEvent;
                    c.Compute(inputs.ToArray());
                }
            }

            int sum = 0;

            for(int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    sum += grid[i, j];
                }
            }

            return sum.ToString();
        }

        int x = 0;
        int y = 0;
        int k = 0;

        bool turn = false;

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            if (turn)
            {
                c.Input = y;
                turn = false;

            }
            else
            {
                c.Input = x;
                turn = true;
            }
        }

        int[,] grid = new int[size, size];

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            grid[x, y] = (int)c.Output;
        }

        public string RunTwo()
        {
            Tuple<int, int> start = new Tuple<int, int>(0, 0);
            int biggest = 0;
            //PrintGrid();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(grid[j, i] == 1)
                    {
                        int a = GetBiggestSquare(j, i);
                        if(a > biggest)
                        {
                            biggest = a;
                            start = new Tuple<int, int>(j, i);
                        }
                        if(a == 100)
                        {
                            return (j*10000 + i).ToString();
                        }
                    }
                }
            }

            return "";
        }

        private int GetBiggestSquare(int x, int y)
        {
            int xSpan = 1;
            int ySpan = 1;

            for (int i = x + 1; i < 9999; i++)
            {
                if(i < size && grid[i, y] == 1)
                {
                    xSpan++;
                }
                else
                {
                    break;
                }
            }

            for (int i = y + 1; i < 9999; i++)
            {
                if(i < size && grid[x, i] == 1)
                {
                    ySpan++;
                }
                else
                {
                    break;
                }
            }

            return Math.Min(xSpan, ySpan);
        }

        private void PrintGrid()
        {
            Console.WriteLine();
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    Console.Write(grid[j, i]);
                }
                Console.WriteLine();
            }
        }
    }
}

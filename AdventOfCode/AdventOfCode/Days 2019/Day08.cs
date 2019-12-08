using System;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day08 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            int width = 25;
            int height = 6;

            int recordZeroes = int.MaxValue;

            int recordLayer = -1;

            int zeros = 0;
            string inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input08.txt").ToList()[0];

            int start = 0;

            for (int i = 0; i < inputs.Length;)
            {
                zeros = 0;
                start = i;
                for (int k = 0; k < height * width; k++)
                {
                    if (int.Parse(inputs[i].ToString()) == 0)
                    {
                        zeros++;
                    }
                    i++;
                }
                if (zeros < recordZeroes)
                {
                    recordZeroes = zeros;
                    recordLayer = start;
                }
            }

            int ones = 0;
            int twos = 0;

            for (int i = recordLayer; ; i++)
            {
                for (int k = 0; k < height * width; k++)
                {
                    if (int.Parse(inputs[i].ToString()) == 1)
                    {
                        ones++;
                    }
                    if (int.Parse(inputs[i].ToString()) == 2)
                    {
                        twos++;
                    }
                    i++;
                }
                break;
            }

            return (ones * twos).ToString();
        }

        public string RunTwo()
        {
            int width = 25;
            int height = 6;

            string inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input08.txt").ToList()[0];

            int[,] grid = new int[25, 6];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    grid[j, i] = -1;
            }

            for (int i = 0; i < inputs.Length;)
            {
                for (int k = 0; k < height; k++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (grid[j, k] == -1 && int.Parse(inputs[i].ToString()) != 2)
                            grid[j, k] = int.Parse(inputs[i].ToString());
                        i++;
                    }
                }
            }
            //Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[j, i] == 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    //Console.Write(grid[j, i]);
                }
                //Console.WriteLine();
            }

            return "FAHEF";
        }
    }
}

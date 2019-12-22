using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day17 : IPuzzle
    {
        public bool Active => true;

        string[,] grid = new string[50, 41];

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input17.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            Computer c = new Computer(0);
            c.OutputEvent += C_OutputEvent;

            c.Compute(inputs.ToArray());

            if (visualize)
                PrintGrid();

            int sum = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    sum += IsIntersectionPoint(grid, i, j);
                }
            }
            if(visualize)
                PrintGrid();

            return sum.ToString();
        }

        bool visualize = false;

        private int IsIntersectionPoint(string[,] grid, int i, int j)
        {
            string middle = GetGridValue(grid, i, j);
            string up = GetGridValue(grid, i - 1, j);
            string right = GetGridValue(grid, i, j + 1);
            string down = GetGridValue(grid, i + 1, j);
            string left = GetGridValue(grid, i, j - 1);

            if (middle == "#" && up == "#" && right == "#" && down == "#" && left == "#")
            {
                grid[i, j] = "O";
                return i * j;
            }

            return 0;
        }

        private string GetGridValue(string[,] grid, int i, int j)
        {
            if (i < 0)
                return ".";
            if (i >= grid.GetLength(0))
                return ".";
            if (j < 0)
                return ".";
            if (j >= grid.GetLength(1))
                return ".";

            return grid[i, j];
        }

        int column = 0;
        int row = 0;

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            if(c.Output == 35)
            {
                grid[row, column] = "#";
                column++;
            }
            else if(c.Output == 46)
            {
                grid[row, column] = ".";
                column++;
            }
            else if(c.Output == 10)
            {
                row++;
                column = 0;
            }
            else if(c.Output == 94)
            {
                grid[row, column] = "^";
                column++;
            }
        }

        private void PrintGrid()
        {
            Console.WriteLine();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input17.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
            i = 0;
            Computer c = new Computer(0);

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            column = 0;
            row = 0;

            c = new Computer(0);

            c.InputEvent += C_InputEvent;

            inputs[0] = 2;

            c.Compute(inputs.ToArray());


            return c.Output.ToString();
        }

        string[] instructions = new string[] { "65", "44", "66", "44", "66", "44", "67", "44", "65", "44", "66", "44", "67", "44", "65", "44", "66", "44", "67", "10",
        //L,6, R,12 L,4 L,6
        "76", "44", "54", "44", "82", "44", "49", "50", "44", "76", "44", "52", "44", "76", "44", "54", "10",

//R,6
//L,6
//R,12

        "82", "44", "54", "44", "76", "44", "54", "44", "82", "44", "49", "50", "10",

//        L,6
//L,10
//L,10
//R,6
        "76", "44", "54", "44", "76", "44", "49", "48", "44", "76", "44", "49", "48", "44", "82", "44", "54", "10",
            
            "110", "10" };

        //A, B, B, C, A, B, C, A, B, C
        //y = 121

        int i = 0;

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            c.Input = int.Parse(instructions[i]);
            i++;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            c = new Computer(0);

            c.OutputEvent += C_OutputEvent;
            c.InputEvent += C_InputEvent;

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

            c.Compute(inputs.ToArray());

            return "";
        }

        string[,] grid = new string[26, 26];

        int x = 13;
        int y = 13;

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                c.Input = 3;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                c.Input = 4;
            }
            else if(key.Key == ConsoleKey.UpArrow)
            {
                c.Input = 1;
            }
            else if(key.Key == ConsoleKey.DownArrow)
            {
                c.Input = 2;
            }
        }


        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            long output = c.Output;

            if(output == 0)
            {
                if(c.Input == 1)
                {
                    grid[y - 1, x] = "#";
                    Console.SetCursorPosition(x, y - 1);
                    Console.Write("#");
                }
                else if(c.Input == 2)
                {

                }
            }
            else if (output == 1)
            {

            }
            else if (output == 2)
            {

            }
            else
            {

            }


        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}

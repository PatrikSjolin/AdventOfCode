using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day11 : IPuzzle
    {
        public bool Active => true;

        static int size = 120;

        int[,] grid;
        int x;
        int y;
        bool move;

        Dictionary<Point, bool> painted = new Dictionary<Point, bool>();

        Dictionary<int, Point> directions = new Dictionary<int, Point>
        {
            { 0, new Point(0, 1) },
            { 1, new Point(1, 0) },
            { 2, new Point(0, -1) },
            { 3, new Point(-1, 0) },
        };

        int direction = 0;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input11.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            grid = new int[size, size];
            x = size / 2;
            y = size / 2;
            move = false;
            direction = 0;

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            Computer c = new Computer(0);
            c.OutputEvent += C_Output;
            c.Compute(inputs.ToArray());

            return painted.Count.ToString();
        }

        public static int Input { get; set; }

        private void C_Output(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            int value = (int)c.Output;

            if (!move)
            {
                if (!painted.Keys.Contains(new Point(x, y)))
                    painted.Add(new Point(x, y), true);
                grid[x, y] = value;
                move = true;
                c.Input = value;
            }
            else if (move)
            {
                if(value == 1)
                {
                    direction = (direction + 1) % 4;
                }
                else
                {
                    direction = (direction - 1) % 4;
                }

                if (direction == -1)
                    direction = 3;

                x += directions[direction].X;
                y += directions[direction].Y;

                c.Input = grid[x, y];

                move = false;
            }
        }

        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input11.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
            
            grid = new int[size, size];
            x = size / 2;
            y = size / 2;
            move = false;
            direction = 0;
            painted.Clear();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }


            Computer c = new Computer(1);
            c.OutputEvent += C_Output;
            c.Compute(inputs.ToArray());

            //Console.WriteLine();
            //for (int i = size - 1; i >= 0; i--)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        if (grid[j, i] == 1)
            //        {
            //            Console.ForegroundColor = ConsoleColor.Red;
            //            Console.Write("\u2588");

            //        }
            //        else
            //        {
            //            Console.ForegroundColor = ConsoleColor.White;
            //            Console.Write(" ");
            //        }
            //    }

            //    Console.WriteLine();
            //}

            return "KRZEAJHB";
        }
    }
}

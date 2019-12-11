using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day11 : IPuzzle
    {
        public bool Active => true;

        static int size = 120;

        int[,] grid = new int[size, size];
        int x = size / 2;
        int y = size / 2;
        bool Move = false;

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
            
            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            Computer c = new Computer();
            c.Input = 0;
            c.OutputEvent += C_Output;
            c.Compute(inputs.ToArray(), 0);

            return painted.Count.ToString();
        }

        public static int Input { get; set; }

        private void C_Output(object sender, EventArgs e)
        {
            int value = (int)((Computer)sender).Output;

            if (!Move)
            {
                if (!painted.Keys.Contains(new Point(x, y)))
                    painted.Add(new Point(x, y), true);
                grid[x, y] = value;
                Move = true;
                ((Computer)sender).Input = value;
            }
            else if (Move)
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

                ((Computer)sender).Input = grid[x, y];

                Move = false;
            }
        }

        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input11.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            grid = new int[size, size];
            x = size / 2;
            y = size / 2;
            Move = false;
            direction = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = 1;
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }


            Computer c = new Computer();
            c.Input = 1;
            c.OutputEvent += C_Output;
            c.Compute(inputs.ToArray(), 0);

            //Console.WriteLine();
            //for (int i = size-1; i >= 0; i--)
            //{
            //    Console.Write("@");
            //    for (int j = 0; j < size; j++)
            //    {
            //        if (grid[j, i] == 1)
            //            Console.ForegroundColor = ConsoleColor.Red;
            //        else
            //            Console.ForegroundColor = ConsoleColor.White;
            //        Console.Write(grid[j, i]);
            //    }

            //    Console.WriteLine();
            //}

            return "KRZEAJHB";
        }
    }
}

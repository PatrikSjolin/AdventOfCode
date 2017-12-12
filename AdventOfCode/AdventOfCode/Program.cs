using AdventOfCode.Days;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Advent of Code 2017!\n");

                Console.WriteLine("Press 0 to quit");
                Console.WriteLine("Press 1 to run all");
                Console.WriteLine("Press 2 to run specific");
                Console.WriteLine("Press 3 to run specific x times");
                Console.WriteLine("Press 4 to benchmark all puzzles");

                Console.WriteLine("\n");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D0:
                        return;

                    case ConsoleKey.D1:
                        List<IPuzzle> puzzles = new List<IPuzzle>
                    {
                        new Day01(),
                        new Day02(),
                        new Day03(),
                        new Day04(),
                        new Day05(),
                        new Day06(),
                        new Day07(),
                        new Day08(),
                        new Day09(),
                        new Day10(),
                        new Day11(),
                        new Day12(),
                        //new Day13(),
                        //new Day14(),
                        //new Day15(),
                        //new Day16(),
                        //new Day17(),
                        //new Day18(),
                        //new Day19(),
                        //new Day20(),
                        //new Day21(),
                        //new Day22(),
                        //new Day23(),
                        //new Day24(),
                        //new Day25(),
                    };

                        int num = 1;

                        foreach (var p in puzzles)
                        {
                            Console.WriteLine("Solution to puzzle {0}", num++);
                            Console.Write("a) ");
                            TimeTask(() => p.RunOne());
                            Console.Write("b) ");
                            TimeTask(() => p.RunTwo());
                            Console.WriteLine();
                        }
                        break;

                    case ConsoleKey.D2:
                        break;

                    case ConsoleKey.D3:
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void TimeTask(Action function)
        {
            DateTime start = DateTime.Now;

            function.Invoke();

            double timeElapsed = DateTime.Now.Subtract(start).TotalMilliseconds;

            Console.WriteLine("Finished in {0} ms", timeElapsed);
        }
    }
}

using AdventOfCode.Days;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, IPuzzle> puzzles = new Dictionary<int, IPuzzle>
            {
                { 1, new Day01() },
                { 2, new Day02() },
                { 3, new Day03() },
                { 4, new Day04() },
                { 5, new Day05() },
                { 6, new Day06() },
                { 7, new Day07() },
                { 8, new Day08() },
                { 9, new Day09() },
                { 10, new Day10() },
                { 11, new Day11() },
                { 12, new Day12() },
                { 13, new Day13() },
                { 14, new Day14() },
                { 15, new Day15() },
                { 16, new Day16() },
                { 17, new Day17() },
                //{ 18, new Day18() },
                //{ 19, new Day19() },
                //{ 20, new Day20() },
                //{ 21, new Day21() },
                //{ 22, new Day22() },
                //{ 23, new Day23() },
                //{ 24, new Day24() },
                //{ 25, new Day25() }
            };

            while (true)
            {
                Console.WriteLine("Advent of Code 2017!\n");

                Console.WriteLine("Press 0 to quit");
                Console.WriteLine("Press 1 to run all");
                Console.WriteLine("Press 2 to run specific");
                Console.WriteLine("Press 3 to benchmark all puzzles");

                Console.Write("\nYour input: ");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        return;

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        foreach (var p in puzzles)
                        {
                            Console.WriteLine("Solution to puzzle {0}", p.Key);
                            Console.Write("a) ");
                            int timeElapsed = 0;
                            timeElapsed = TimeTask(() => p.Value.RunOne());
                            Console.WriteLine("Finished in {0} ms", timeElapsed);
                            Console.Write("b) ");
                            timeElapsed = TimeTask(() => p.Value.RunTwo());
                            Console.WriteLine("Finished in {0} ms", timeElapsed);
                            Console.WriteLine();
                        }
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            Console.Write("\nEnter puzzle number: ");
                            int puzzleNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Solution to puzzle {0}", puzzleNumber);
                            Console.Write("a) ");
                            int timeElapsed = 0;
                            timeElapsed = TimeTask(() => puzzles[puzzleNumber].RunOne());

                            Console.WriteLine("Finished in {0} ms", timeElapsed);
                            Console.Write("b) ");
                            timeElapsed = TimeTask(() => puzzles[puzzleNumber].RunTwo());

                            Console.WriteLine("Finished in {0} ms", timeElapsed);
                            Console.WriteLine();
                            break;
                        }
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            Console.Write("\nEnter number of runs: ");

                            int runs = int.Parse(Console.ReadLine());
                            Dictionary<int, int> times = new Dictionary<int, int>();

                            foreach (var p in puzzles)
                            {
                                int sum = 0;
                                for (int i = 0; i < runs; i++)
                                {
                                    sum += TimeTask(() => p.Value.RunOne(), true);
                                    sum += TimeTask(() => p.Value.RunTwo(), true);
                                }
                                times.Add(p.Key, sum);
                            }

                            foreach (var time in times)
                            {
                                Console.WriteLine("Puzzle {0} averaged in {1} ms", time.Key, time.Value / runs);
                            }

                            Console.WriteLine("\nTotal time: {0} ms", times.Values.Sum() / runs);

                            break;
                        }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static int TimeTask(Func<string> function, bool silent = false)
        {
            DateTime start = DateTime.Now;

            var result = function.Invoke();
            if (!silent)
                Console.WriteLine(result);

            int timeElapsed = (int)DateTime.Now.Subtract(start).TotalMilliseconds;

            return timeElapsed;
        }
    }
}

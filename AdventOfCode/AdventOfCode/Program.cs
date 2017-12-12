using AdventOfCode.Days;
using System;
using System.Collections.Generic;

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
                            //{ 13, new Day13() },
                            //{ 14, new Day14() },
                            //{ 15, new Day15() },
                            //{ 16, new Day16() },
                            //{ 17, new Day17() },
                            //{ 18, new Day18() },
                            //{ 19, new Day19() },
                            //{ 20, new Day20() },
                            //{ 21, new Day21() },
                            //{ 22, new Day22() },
                            //{ 23, new Day23() },
                            //{ 24, new Day24() },
                            //{ 25, new Day25() }
                        };

            int num = 1;

            while (true)
            {
                Console.WriteLine("Advent of Code 2017!\n");

                Console.WriteLine("Press 0 to quit");
                Console.WriteLine("Press 1 to run all");
                Console.WriteLine("Press 2 to run specific");
                Console.WriteLine("Press 3 to run specific x times");
                Console.WriteLine("Press 4 to benchmark all puzzles");

                Console.Write("\nYour input: ");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D0:
                        return;

                    case ConsoleKey.D1:
                        foreach (var p in puzzles)
                        {
                            Console.WriteLine("Solution to puzzle {0}", num++);
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
                        {
                            Console.Write("\nEnter puzzle number: ");
                            int puzzleNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Solution to puzzle {0}", num++);
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
                        {
                            Console.Write("\nEnter puzzle number: ");
                            int puzzleNumber = int.Parse(Console.ReadLine());
                            Console.Write("\nEnter number of runs: ");
                            int runs = int.Parse(Console.ReadLine());
                            int sum = 0;
                            for(int i = 0; i < runs; i++)
                            {
                                sum += TimeTask(() => puzzles[puzzleNumber].RunOne());
                                sum += TimeTask(() => puzzles[puzzleNumber].RunTwo());
                            }
                            Console.WriteLine("Total time spent: {0} ms", sum);
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            Console.Write("\nEnter number of runs: ");

                            int runs = int.Parse(Console.ReadLine());
                            Dictionary<int, int> times = new Dictionary<int, int>();

                            foreach (var p in puzzles)
                            {
                                int sum = 0;
                                for (int i = 0; i < runs; i++)
                                {
                                    sum += TimeTask(() => p.Value.RunOne());
                                    sum += TimeTask(() => p.Value.RunTwo());
                                }
                                times.Add(p.Key, sum);
                            }

                            foreach(var time in times)
                            {
                                Console.WriteLine("Puzzle {0} averaged in {1} ms", time.Key, time.Value / runs);
                            }

                            break;
                        }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static int TimeTask(Action function)
        {
            DateTime start = DateTime.Now;

            function.Invoke();

            int timeElapsed = (int)DateTime.Now.Subtract(start).TotalMilliseconds;

            return timeElapsed;
        }
    }
}

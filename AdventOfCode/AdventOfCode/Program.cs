using AdventOfCode.Days;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Cleanse()
        {
            List<string> input2017 = System.IO.File.ReadAllLines(@"..\..\Data\2018\2017.txt").ToList();
            List<string> input2018 = System.IO.File.ReadAllLines(@"..\..\Data\2018\2018.txt").ToList();

            List<string> zeroStars2017 = new List<string>();
            List<string> zeroStars2018 = new List<string>();

            foreach (var v in input2017)
            {
                List<string> splitList = v.Split(' ').ToList();
                if (splitList[4] == "0")
                {
                    if (splitList.Contains("(anonymous"))
                        zeroStars2017.Add(splitList[7] + " " + splitList[8] + " " + splitList[9]);
                    else
                        zeroStars2017.Add(splitList[7] + " " + splitList[8]);
                }
            }

            foreach (var v in input2018)
            {
                List<string> splitList = v.Split(' ').ToList();
                if (splitList[4] == "0")
                {
                    if (splitList.Contains("(anonymous"))
                        zeroStars2018.Add(splitList[7] + " " + splitList[8] + " " + splitList[9]);
                    else
                        zeroStars2018.Add(splitList[7] + " " + splitList[8]);
                }
            }

            foreach (var v in zeroStars2017)
            {
                if (zeroStars2018.Contains(v))
                {
                    Console.WriteLine(v);
                }
            }
        }

        static void Main(string[] args)
        {
            //Cleanse();
            Dictionary<int, IPuzzle> puzzles;

            while (true)
            {
                int year = 2018;

                puzzles = GetPuzzles(year);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Advent of Code {0}!\n", year);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press 0 to quit");
                Console.WriteLine("Press 1 to run all");
                Console.WriteLine("Press 2 to run specific");
                Console.WriteLine("Press 3 to benchmark all puzzles");
                Console.WriteLine("Press 4 to benchmark all puzzles (detailed)");
                Console.WriteLine("Press 5 to benchmark specific puzzles");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nYour input: ");
                Console.ForegroundColor = ConsoleColor.White;

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
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nAverage times after {0} runs:", runs);
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var time in times)
                            {
                                Console.WriteLine("Puzzle {0}: {1} ms", time.Key < 10 ? ("0" + time.Key) : time.Key.ToString(), time.Value / runs);
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nTotal time: {0} ms", times.Values.Sum() / runs);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        {
                            Console.Write("\nEnter number of runs: ");

                            int runs = int.Parse(Console.ReadLine());
                            Dictionary<int, Point> times = new Dictionary<int, Point>();

                            foreach (var p in puzzles)
                            {
                                Point point = new Point(0, 0);
                                for (int i = 0; i < runs; i++)
                                {
                                    point.X = TimeTask(() => p.Value.RunOne(), true);
                                    point.Y = TimeTask(() => p.Value.RunTwo(), true);
                                }
                                times.Add(p.Key, point);
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nAverage times after {0} runs:", runs);
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var time in times)
                            {
                                Console.Write("Puzzle {0}: {1} ms", time.Key < 10 ? ("0" + time.Key) : time.Key.ToString(), (time.Value.X + time.Value.Y) / runs);
                                Console.Write(" (A) {0} ms, ", time.Value.X);
                                Console.WriteLine("B) {0} ms)", time.Value.Y);
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nTotal time: {0} ms", times.Values.Sum(x => x.X + x.Y) / runs);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        {
                            Console.Write("\nEnter puzzle number: ");
                            int puzzleNumber = int.Parse(Console.ReadLine());


                            Console.Write("\nEnter number of runs: ");
                            int runs = int.Parse(Console.ReadLine());
                            List<Point> times = new List<Point>();

                            for(int i = 0; i < runs; i++)
                            {
                                Point p = new Point(0, 0);
                                p.X = TimeTask(() => puzzles[puzzleNumber].RunOne(), true);
                                p.Y = TimeTask(() => puzzles[puzzleNumber].RunTwo(), true);
                                times.Add(p);
                            }

                            int sumX = times.Sum(x => x.X);
                            int sumY = times.Sum(x => x.Y);

                            Console.WriteLine("\nAverage times after {0} runs:", runs);
                            Console.WriteLine("A: {0} ms", sumX / runs);
                            Console.WriteLine("B: {0} ms", sumY / runs);
                            Console.WriteLine();
                            break;
                        }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static Dictionary<int, IPuzzle> GetPuzzles(int year)
        {
            if (year == 2015)
            {
                return new Dictionary<int, IPuzzle>
                {
                    { 1, new Days_2015.Day01() },
                    { 2, new Days_2015.Day02() },
                    { 3, new Days_2015.Day03() },
                    { 4, new Days_2015.Day04() },
                    { 5, new Days_2015.Day05() },
                    { 6, new Days_2015.Day06() },
                    { 7, new Days_2015.Day07() },
                };
            }
            if (year == 2017)
            {
                return new Dictionary<int, IPuzzle>
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
                    { 18, new Day18() },
                    { 19, new Day19() },
                    { 20, new Day20() },
                    { 21, new Day21() },
                    { 22, new Day22() },
                    { 23, new Day23() },
                    { 24, new Day24() },
                    { 25, new Day25() }
                };
            }
            if (year == 2018)
            {
                return new Dictionary<int, IPuzzle>
                {
                    { 1, new Days_2018.Day01() },
                    { 2, new Days_2018.Day02() },
                    { 3, new Days_2018.Day03() },
                    { 4, new Days_2018.Day04() },
                    { 5, new Days_2018.Day05() },
                    { 6, new Days_2018.Day06() },
                    { 7, new Days_2018.Day07() },
                    { 8, new Days_2018.Day08() },
                    { 9, new Days_2018.Day09() },
                    { 10, new Days_2018.Day10() },
                    { 11, new Days_2018.Day11() },
                    { 12, new Days_2018.Day12() },
                };
            }
            return null;
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

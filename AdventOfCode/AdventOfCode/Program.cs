using AdventOfCode.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

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
            Dictionary<int, string> solutions;

            while (true)
            {
                int year = int.Parse(ConfigurationManager.AppSettings["Year"]);

                puzzles = GetPuzzles(year);
                solutions = GetSolutions(year);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Advent of Code {0}!\n", year);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press 0 to quit");
                Console.WriteLine("Press 1 to run & verify all");
                Console.WriteLine("Press 2 to run specific");
                Console.WriteLine("Press 3 to benchmark all puzzles");
                Console.WriteLine("Press 4 to benchmark all puzzles (detailed)");
                Console.WriteLine("Press 5 to benchmark specific puzzles");
                Console.WriteLine("Press 6 to change year");

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
                        Console.WriteLine("\n");
                        foreach (var p in puzzles.Where(x => x.Value.Active))
                        {
                            Console.WriteLine("Testing puzzle {0}", p.Key);
                            Console.Write("a) ");
                            double timeElapsed = 0;
                            string result = "";
                            timeElapsed = TimeTask(() => result = p.Value.RunOne(), true);
                            if (result != solutions[p.Key].Split(';')[0])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Test failed");
                                Console.WriteLine("Result was {0} and should be {1}", result, solutions[p.Key].Split(';')[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Test passed");
                                Console.WriteLine("Result was {0} and should be {1}", result, solutions[p.Key].Split(';')[0]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            Console.WriteLine("Finished in {0:0.00} ms", timeElapsed);
                            Console.WriteLine();

                            Console.Write("b) ");
                            timeElapsed = TimeTask(() => result = p.Value.RunTwo(), true);

                            if (result != solutions[p.Key].Split(';')[1])
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Test failed");
                                Console.WriteLine("Result was {0} and should be {1}", result, solutions[p.Key].Split(';')[1]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Test passed");
                                Console.WriteLine("Result was {0} and should be {1}", result, solutions[p.Key].Split(';')[1]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            Console.WriteLine("Finished in {0:0.00} ms", timeElapsed);
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
                            double timeElapsed = 0;
                            timeElapsed = TimeTask(() => puzzles[puzzleNumber].RunOne());

                            Console.WriteLine("Finished in {0:0.00} ms", timeElapsed);
                            Console.Write("b) ");
                            timeElapsed = TimeTask(() => puzzles[puzzleNumber].RunTwo());

                            Console.WriteLine("Finished in {0:0.00} ms", timeElapsed);
                            Console.WriteLine();
                            break;
                        }
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            Console.Write("\nEnter number of runs: ");

                            int runs = int.Parse(Console.ReadLine());
                            Dictionary<int, double> times = new Dictionary<int, double>();

                            foreach (var p in puzzles.Where(x => x.Value.Active))
                            {
                                double sum = 0;
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
                                Console.WriteLine("Puzzle {0}: {1:0.00} ms", time.Key < 10 ? ("0" + time.Key) : time.Key.ToString(), time.Value / runs);
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nTotal time: {0:0.00} ms", times.Values.Sum() / runs);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        {
                            Console.Write("\nEnter number of runs: ");

                            //int runs = int.Parse(Console.ReadLine());
                            //Dictionary<int, Tuple<decimal, decimal>> times = new Dictionary<int, Tuple<decimal,>();

                            //foreach (var p in puzzles.Where(x => x.Value.Active))
                            //{
                            //    Point point = new Point(0, 0);
                            //    for (int i = 0; i < runs; i++)
                            //    {
                            //        point.X = TimeTask(() => p.Value.RunOne(), true);
                            //        point.Y = TimeTask(() => p.Value.RunTwo(), true);
                            //    }
                            //    times.Add(p.Key, point);
                            //}
                            //Console.ForegroundColor = ConsoleColor.Green;
                            //Console.WriteLine("\nAverage times after {0} runs:", runs);
                            //Console.ForegroundColor = ConsoleColor.White;
                            //foreach (var time in times)
                            //{
                            //    Console.Write("Puzzle {0}: {1} ms", time.Key < 10 ? ("0" + time.Key) : time.Key.ToString(), (time.Value.X + time.Value.Y) / runs);
                            //    Console.Write(" (A) {0} ms, ", time.Value.X);
                            //    Console.WriteLine("B) {0} ms)", time.Value.Y);
                            //}

                            //Console.ForegroundColor = ConsoleColor.Green;
                            //Console.WriteLine("\nTotal time: {0} ms", times.Values.Sum(x => x.X + x.Y) / runs);
                            //Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        {
                            //Console.Write("\nEnter puzzle number: ");
                            //int puzzleNumber = int.Parse(Console.ReadLine());


                            //Console.Write("\nEnter number of runs: ");
                            //int runs = int.Parse(Console.ReadLine());
                            //List<Point> times = new List<Point>();

                            //for (int i = 0; i < runs; i++)
                            //{
                            //    Point p = new Point(0, 0);
                            //    p.X = TimeTask(() => puzzles[puzzleNumber].RunOne(), true);
                            //    p.Y = TimeTask(() => puzzles[puzzleNumber].RunTwo(), true);
                            //    times.Add(p);
                            //}

                            //int sumX = times.Sum(x => x.X);
                            //int sumY = times.Sum(x => x.Y);

                            //Console.WriteLine("\nAverage times after {0} runs:", runs);
                            //Console.WriteLine("A: {0} ms", sumX / runs);
                            //Console.WriteLine("B: {0} ms", sumY / runs);
                            //Console.WriteLine();
                            break;
                        }

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        {
                            Console.Write("\nEnter year: ");
                            int newYear = int.Parse(Console.ReadLine());

                            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                            config.AppSettings.Settings["Year"].Value = newYear.ToString();
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");
                            Console.WriteLine("Year changes to {0}", newYear);
                            break;
                        }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static Dictionary<int, string> GetSolutions(int year)
        {
            if (year == 2015)
            {
                return new Dictionary<int, string>
                {
                    { 1, "232;1783" },
                    { 2, "1606483;3842356" },
                    {3, "2592;2360" },
                    {4, "117946;3938038" },
                    {5, "258;53" },
                    {6, "543903;14687245" },
                    { 7, "46065;14134" },
                };
            }
            if (year == 2016)
            {
                return new Dictionary<int, string>
                {
                    { 1, "273;115" },
                    {2, "56983;8B8B1" },
                    {3, "993;1849" },
                    {4, "185371;984" },
                    {13, "86;127" },
                    //{2, "7105;omlvgdokxfncvqyersasjziup" },
                    //{3, "112378;603" },
                    //{4, "21956;134511" },
                    //{5, "11108;5094" },
                    //{6, "3449;44868" },
                    //{7, "EPWCFXKISTZVJHDGNABLQYMORU;952" },
                    //{8, "36891;20083" },
                    //{9, "424112;3487352628" },
                    //{10, "ERCXLAJL;10813" },
                    //{11, "243,64;90,101,15" },
                    //{12, "4110;2650000000466" },
                    //{13, "76,108;2,84" },
                    //{14, "9211134315;20357548" },
                    //{15, "195811;69867" },
                    //{16, "560;622" }
                };
            }
            if (year == 2017)
            {
                return new Dictionary<int, string>
                {
                    { 1, "1141;950" },
                    { 2, "37923;263" },
                    { 3, "438;266330" },
                    { 4, "325;119" },
                    { 5, "376976;29227751" },
                    { 6, "12841;8038" },
                    { 7, "mkxke;268" },
                    { 8, "5102;6056" },
                    { 9, "14190;7053" },
                    { 10, "38628;e1462100a34221a7f0906da15c1c979a" },
                    { 11, "773;1560" },
                    { 12, "134;193" },
                    { 13, "1504;3823370" },
                    { 14, "8214;1093" },
                    { 15, "609;253" },
                    { 16, "padheomkgjfnblic;bfcdeakhijmlgopn" },
                    { 17, "1487;25674054" },
                    { 18, "3188;7112" },
                    { 19, "GPALMJSOY;16204" },
                    { 20, "300;502" },
                    { 21, "152;1956174" },
                    { 22, "5450;2511957" },
                    { 23, "3969;917" },
                    { 24, "1511;1471" },
                    { 25, "2725;All puzzles completed!" },
                };
            }
            if (year == 2018)
            {
                return new Dictionary<int, string>
                {
                    { 1, "433;256" },
                    { 2, "7105;omlvgdokxfncvqyersasjziup" },
                    { 3, "112378;603" },
                    { 4, "21956;134511" },
                    { 5, "11108;5094" },
                    { 6, "3449;44868" },
                    { 7, "EPWCFXKISTZVJHDGNABLQYMORU;952" },
                    { 8, "36891;20083" },
                    { 9, "424112;3487352628" },
                    { 10, "ERCXLAJL;10813" },
                    { 11, "243,64;90,101,15" },
                    { 12, "4110;2650000000466" },
                    { 13, "76,108;2,84" },
                    { 14, "9211134315;20357548" },
                    { 15, "195811;69867" },
                    { 16, "560;622" },
                    { 17, "31934;24790" },
                    { 18, "614812;212176" },
                    { 19, "1824;21340800" },
                    { 20, "3991;8394" },
                    { 21, "5745418;5090905" }
                };
            }
            if (year == 2019)
            {
                return new Dictionary<int, string>
                {
                    { 1, "3231941;4845049" },
                    { 2, "7594646;3376" },
                    { 3, "731;5672" },
                    { 4, "2220;1515" },
                    { 5, "7259358;11826654" },
                    { 6, "194721;316" },
                    { 7, "225056;14260332" },
                    { 8, "1330;FAHEF" },
                    { 9, "2870072642;58534" },
                    { 10, "278;1417" },
                    { 11, "2054;KRZEAJHB" },
                    { 12, "7988;337721412394184" },
                    { 13, "376;18509" },
                    { 14, "143173;8845261" },
                    { 15, "234;292" },
                    { 16, "44098263;12482168" },
                    { 17, "6052;752491" },
                    { 18, ";" },
                    { 19, "129;14040699" },
                    { 20, ";" },
                    { 21, "19354173;1145849660" },
                    { 22, ";" },
                    { 23, "17714;10982" },
                    { 24, "32509983;2012" },
                    { 25, "2147502592;All puzzles completed!" },
                };
            }
            return null;
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
            if (year == 2016)
            {
                return new Dictionary<int, IPuzzle>
                {
                    { 1, new Days_2016.Day01() },
                    { 2, new Days_2016.Day02() },
                    { 3, new Days_2016.Day03() },
                    { 4, new Days_2016.Day04() },
                    { 5, new Days_2016.Day05() },
                    { 6, new Days_2016.Day06() },
                    { 7, new Days_2016.Day07() },
                    { 8, new Days_2016.Day08() },
                    { 9, new Days_2016.Day09() },
                    { 10, new Days_2016.Day10() },
                    { 11, new Days_2016.Day11() },
                    { 12, new Days_2016.Day12() },
                    { 13, new Days_2016.Day13() },
                    { 14, new Days_2016.Day14() },
                    { 15, new Days_2016.Day15() },
                    { 16, new Days_2016.Day16() },
                    { 17, new Days_2016.Day17() },
                    { 18, new Days_2016.Day18() },
                    { 19, new Days_2016.Day19() },
                    { 20, new Days_2016.Day20() },
                    { 21, new Days_2016.Day21() },
                    { 22, new Days_2016.Day22() },
                    { 23, new Days_2016.Day23() },
                    { 24, new Days_2016.Day24() },
                    { 25, new Days_2016.Day25() }
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
                    { 13, new Days_2018.Day13() },
                    { 14, new Days_2018.Day14() },
                    { 15, new Days_2018.Day15() },
                    { 16, new Days_2018.Day16() },
                    { 17, new Days_2018.Day17() },
                    { 18, new Days_2018.Day18() },
                    { 19, new Days_2018.Day19() },
                    { 20, new Days_2018.Day20() },
                    { 21, new Days_2018.Day21() },
                    { 22, new Days_2018.Day22() },
                    { 23, new Days_2018.Day23() },
                    { 24, new Days_2018.Day24() },
                    { 25, new Days_2018.Day25() }
                };
            }
            if (year == 2019)
            {
                return new Dictionary<int, IPuzzle>
                {
                    { 1, new Days_2019.Day01() },
                    { 2, new Days_2019.Day02() },
                    { 3, new Days_2019.Day03() },
                    { 4, new Days_2019.Day04() },
                    { 5, new Days_2019.Day05() },
                    { 6, new Days_2019.Day06() },
                    { 7, new Days_2019.Day07() },
                    { 8, new Days_2019.Day08() },
                    { 9, new Days_2019.Day09() },
                    { 10, new Days_2019.Day10() },
                    { 11, new Days_2019.Day11() },
                    { 12, new Days_2019.Day12() },
                    { 13, new Days_2019.Day13() },
                    { 14, new Days_2019.Day14() },
                    { 15, new Days_2019.Day15() },
                    { 16, new Days_2019.Day16() },
                    { 17, new Days_2019.Day17() },
                    { 18, new Days_2019.Day18() },
                    { 19, new Days_2019.Day19() },
                    { 20, new Days_2019.Day20() },
                    { 21, new Days_2019.Day21() },
                    { 22, new Days_2019.Day22() },
                    { 23, new Days_2019.Day23() },
                    { 24, new Days_2019.Day24() },
                    { 25, new Days_2019.Day25() }
                };
            }
            if(year == 2020)
            {
                return new Dictionary<int, IPuzzle>
                {   { 1, new Days_2020.Day01() },
                    { 2, new Days_2020.Day02() },
                    { 3, new Days_2020.Day03() },
                    { 4, new Days_2020.Day04() },
                    { 5, new Days_2020.Day05() },
                    //{ 6, new Days_2020.Day06() },
                    //{ 7, new Days_2020.Day07() },
                    //{ 8, new Days_2020.Day08() },
                    //{ 9, new Days_2020.Day09() },
                    //{ 10, new Days_2020.Day10() },
                    //{ 11, new Days_2020.Day11() },
                    //{ 12, new Days_2020.Day12() },
                    //{ 13, new Days_2020.Day13() },
                    //{ 14, new Days_2020.Day14() },
                    //{ 15, new Days_2020.Day15() },
                    //{ 16, new Days_2020.Day16() },
                    //{ 17, new Days_2020.Day17() },
                    //{ 18, new Days_2020.Day18() },
                    //{ 19, new Days_2020.Day19() },
                    //{ 20, new Days_2020.Day20() },
                    //{ 21, new Days_2020.Day21() },
                    //{ 22, new Days_2020.Day22() },
                    //{ 23, new Days_2020.Day23() },
                    //{ 24, new Days_2020.Day24() },
                    //{ 25, new Days_2020.Day25() }
                };
            }
            return null;
        }

        static double TimeTask(Func<string> function, bool silent = false)
        {
            DateTime start = DateTime.Now;

            var result = function.Invoke();
            if (!silent)
                Console.WriteLine(result);

            double timeElapsed = DateTime.Now.Subtract(start).TotalMilliseconds;

            return timeElapsed;
        }
    }
}

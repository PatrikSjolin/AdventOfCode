using AdventOfCode.Days;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2017!\n");

            List<IPuzzle> puzzles = new List<IPuzzle>
            {
                new Day01(),
                new Day02(),
                new Day03(),
                new Day04(),
                new Day05(),
                new Day06(),
                new Day07(),
                //new Day08(),
                //new Day09(),
                //new Day10(),
                //new Day011(),
                //new Day012(),
                //new Day013(),
                //new Day014(),
                //new Day015(),
                //new Day016(),
                //new Day017(),
                //new Day018(),
                //new Day019(),
                //new Day020(),
                //new Day021(),
                //new Day022(),
                //new Day023(),
                //new Day024(),
                //new Day025(),
            };

            int num = 1;

            foreach(var p in puzzles)
            {
                Console.WriteLine("Solution to puzzle {0}", num++);
                Console.Write("a) ");
                TimeTask(() => p.RunOne());
                Console.Write("b) ");
                TimeTask(() => p.RunTwo());
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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

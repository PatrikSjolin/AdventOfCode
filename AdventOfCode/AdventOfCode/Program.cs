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
                new _1(),
                new _2(),
                new _3(),
                new _4(),
                new _5(),
                new _6(),
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

            if (timeElapsed < 20)
            {
                timeElapsed = 0;
            }

            Console.WriteLine("Finished in {0} ms", timeElapsed);
        }
    }
}

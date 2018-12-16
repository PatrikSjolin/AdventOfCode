using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day19 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {
            //Josepheus problem
            //Math solution
            int input = 3014387;
            int biggestPower = (int)Math.Log(input, 2);
            int biggestPowerOfTwo = (int)Math.Pow(2, biggestPower);
            int rest = input - biggestPowerOfTwo;
            int winner = rest * 2 + 1;

            Console.WriteLine(winner);

            //Programming solution - Could have solved with queue instead but didn't.
            List<int> elves = new List<int>();
            for (int i = 1; i <= input; i++)
            {
                elves.Add(i);
            }

            while (elves.Count() != 1)
            {
                List<int> copy = new List<int>();

                for (int i = 0; i < elves.Count(); i += 2)
                {
                    int stealer = elves[i];
                    int victim = elves[(i + 1) % elves.Count()];
                    copy.Add(stealer);
                }

                if (elves.Count % 2 != 0)
                {
                    copy.Remove(copy.First());
                }

                elves = copy;
            }

            Console.WriteLine(elves.First());
        }

        public void GoTwo()
        {
            int input = 3014387;

            //int test = 100;

            //for (int i = 1; i < test; i++)
            //{


            List<int> elves = new List<int>();

                for (int j = 1; j <= input; j++)
                {
                    elves.Add(j);
                }

                while (elves.Count() != 1)
                {
                    for (int j = 0; j < elves.Count(); j++)
                    {
                        int index = ((elves.Count() / 2) + j) % elves.Count();

                        if (index <= j)
                            j--;


                        //foreach (var elf in elves)
                        //{
                        //    Console.ForegroundColor = ConsoleColor.White;
                        //    if (elf == (i + 1))
                        //        Console.ForegroundColor = ConsoleColor.Green;
                        //    if (elf == (index + 1))
                        //        Console.ForegroundColor = ConsoleColor.Red;

                        //    Console.Write(elf + " ");
                        //}
                        //Console.WriteLine();
                        //Console.ReadLine();


                        elves.RemoveAt(index);
                    }
                }
                Console.WriteLine(elves.First());
            //}
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day09 : IPuzzle
    {
        public void RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input9.txt").ToList();

            string input = inputLines[0];

            int test = CountGroups(0, input.Length, input);

            Console.WriteLine(test);
        }

        private int CountGroups(int v1, int v2, string input)
        {
            int garbageCount = 0;
            int score = 0;
            int count = 0;
            char previous = input[v1];
            bool garbage = false;
            for (int i = v1; i < v2; i++)
            {
                if (previous != '!')
                {
                    if(input[i] == '<' && !garbage)
                    {
                        garbage = true;
                        continue;
                    }
                    else if(garbage && input[i] == '>')
                    {
                        garbage = false;
                        continue;
                    }
                    else if (garbage && input[i] != '!')
                    {
                        garbageCount++;
                    }

                    if (!garbage)
                    {
                        if (input[i] == '{')
                        {
                            count++;
                        }
                        else if (input[i] == '}')
                        {
                            score += count;
                            count--;
                        }
                    }
                    previous = input[i];
                }
                else
                {
                    previous = 'a';
                }
            }

            return score;
        }

        private int CountGarbage(int v1, int v2, string input)
        {
            int garbageCount = 0;
            int score = 0;
            int count = 0;
            char previous = input[v1];
            bool garbage = false;
            for (int i = v1; i < v2; i++)
            {
                if (previous != '!')
                {
                    if (input[i] == '<' && !garbage)
                    {
                        garbage = true;
                        continue;
                    }
                    else if (garbage && input[i] == '>')
                    {
                        garbage = false;
                        continue;
                    }
                    else if (garbage && input[i] != '!')
                    {
                        garbageCount++;
                    }

                    if (!garbage)
                    {
                        if (input[i] == '{')
                        {
                            count++;
                        }
                        else if (input[i] == '}')
                        {
                            score += count;
                            count--;
                        }
                    }
                    previous = input[i];
                }
                else
                {
                    previous = 'a';
                }
            }

            return garbageCount;
        }

        public void RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input9.txt").ToList();

            string input = inputLines[0];

            int test = CountGarbage(0, input.Length, input);

            Console.WriteLine(test);
        }
    }
}

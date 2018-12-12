using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day12 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input12.txt").ToList();
            string inputState = inputLines[0].Split(' ')[2];

            string state = "";

            int extraInBeginning = 500;

            for(int i = 0; i < extraInBeginning; i++)
            {
                state += ".";
            }

            foreach(var s in inputState)
            {
                state += s.ToString();
            }
            int extraInTheEnd = 500;
            for (int i = 0; i < extraInTheEnd; i++)
            {
                state += ".";
            }

            Dictionary<string, string> rules = new Dictionary<string, string>();

            for(int i = 2; i < inputLines.Count; i++)
            {
                rules.Add(inputLines[i].Substring(0, 5), inputLines[i].Substring(9, 1));
            }

            Console.WriteLine();
            Console.WriteLine("0: " + state);

            int prev = 0;
            for (long i = 0; i < 50000000000; i++)
            {
                string newState = "..";

                for(int j = 2; j < state.Length- 2; j++)
                {
                    string input = state.Substring(j-2, 5);
                    string output = ".";
                    if (rules.ContainsKey(input))
                    {
                        output = rules[input];
                    }
                    newState += output;
                }
                if(newState != string.Empty)
                    state = newState;
                else
                {

                }
                state += "..";
                int sums = 0;

                for (int ir = 0; ir < state.Length; ir++)
                {
                    if (state[ir] == '#')
                    {
                        sums += ir - extraInBeginning;
                    }
                }
                Console.WriteLine(sums + " +" + (sums-prev));
                prev = sums;
                //Console.WriteLine((i+1) + ": " + state);
            }

            int sum = 0;

            for(int i = 0; i < state.Length; i++)
            {
                if(state[i] == '#')
                {
                    sum += i - extraInBeginning;
                }
            }


            return sum.ToString();
        }

        public string RunTwo()
        {
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day08 : IPuzzle
    {
        private class Result
        {
            public bool Success { get; set; }
            public int Accumulator { get; set; }
        }

        public bool Active => true;

        private List<(string, int)> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input08.txt").Select(x => x.Split(' ')).Select(y => (y[0], int.Parse(y[1]))).ToList();

            Result result = ExecuteProgram(inputs);
            
            return result.Accumulator.ToString();
        }

        public string RunTwo()
        {
            for (int j = 0; j < inputs.Count; j++)
            {
                List<(string, int)> inputs2 = new List<(string, int)>(inputs);

                while (!(inputs2[j].Item1 == "jmp") && !(inputs2[j].Item1 == "nop"))
                {
                    j++;
                }

                if (inputs2[j].Item1 == "jmp")
                    inputs2[j] = ("nop", inputs2[j].Item2);
                else
                    inputs2[j] = ("jmp", inputs2[j].Item2);

                Result result = ExecuteProgram(inputs2);

                if (result.Success)
                {
                    return result.Accumulator.ToString();
                }
            }

            return "";
        }

        private Result ExecuteProgram(List<(string, int)> program)
        {
            int accumulator = 0;

            HashSet<int> instructions = new HashSet<int>();

            for (int i = 0; i < program.Count;)
            {
                string operation = program[i].Item1;

                if (instructions.Contains(i))
                {
                    return new Result { Success = false, Accumulator = accumulator };
                }
                else
                {
                    instructions.Add(i);
                }

                if (operation == "nop")
                {
                    i++;
                }
                else if (operation == "acc")
                {
                    int argument = program[i].Item2;
                    accumulator += argument;
                    i++;
                }
                else if (operation == "jmp")
                {
                    int argument = program[i].Item2;
                    i += argument;
                }
            }

            return new Result { Success = true, Accumulator = accumulator };
        }
    }
}

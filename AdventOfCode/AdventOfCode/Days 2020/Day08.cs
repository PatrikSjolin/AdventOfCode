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

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input08.txt").ToList();

            Result result = ExecuteProgram(inputs);
            
            return result.Accumulator.ToString();
        }

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input08.txt").ToList();
            for (int j = 0; j < inputs.Count; j++)
            {
                List<string> inputs2 = new List<string>(inputs);

                while (!inputs2[j].StartsWith("jmp") && !inputs2[j].StartsWith("nop"))
                {
                    j++;
                }

                if (inputs2[j].StartsWith("jmp"))
                {
                    int argument = int.Parse(inputs2[j].Split(' ')[1]);
                    inputs2[j] = "nop " + argument;
                }
                else
                {
                    int argument = int.Parse(inputs2[j].Split(' ')[1]);
                    inputs2[j] = "jmp " + argument;
                }

                Result result = ExecuteProgram(inputs2);

                if (result.Success)
                {
                    return result.Accumulator.ToString();
                }
            }

            return "";
        }

        private Result ExecuteProgram(List<string> program)
        {
            int accumulator = 0;

            HashSet<int> instructions = new HashSet<int>();

            for (int i = 0; i < program.Count;)
            {
                string operation = program[i].Split(' ')[0];

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
                    int argument = int.Parse(program[i].Split(' ')[1]);
                    accumulator += argument;
                    i++;
                }
                else if (operation == "jmp")
                {
                    int argument = int.Parse(program[i].Split(' ')[1]);
                    i += argument;
                }
            }

            return new Result { Success = true, Accumulator = accumulator };
        }
    }
}

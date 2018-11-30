using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day08 : IPuzzle
    {
        Dictionary<string, int> registers;
        int max;

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input8.txt").ToList();
            max = 0;
            registers = new Dictionary<string, int>();
            foreach(var s in inputLines)
            {
                List<string> split = s.Split(' ').ToList();

                string register = split[0];
                string condReg = split[4];

                if (!registers.ContainsKey(register))
                {
                    registers.Add(register, 0);
                }
                if (!registers.ContainsKey(condReg))
                {
                    registers.Add(condReg, 0);
                }

                int value = int.Parse(split[2]);

                string op = split[1];

                string condOp = split[5];
                int condVal = int.Parse(split[6]);

                if(condOp == ">")
                {
                    if(registers[condReg] > condVal)
                    {
                        if(op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }
                else if(condOp == "<")
                {
                    if (registers[condReg] < condVal)
                    {
                        if (op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }
                else if(condOp == "<=")
                {
                    if (registers[condReg] <= condVal)
                    {
                        if (op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }
                else if(condOp == ">=")
                {
                    if (registers[condReg] >= condVal)
                    {
                        if (op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }
                else if(condOp == "==")
                {
                    if (registers[condReg] == condVal)
                    {
                        if (op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }
                else if(condOp == "!=")
                {
                    if (registers[condReg] != condVal)
                    {
                        if (op == "inc")
                        {
                            registers[register] += value;
                        }
                        else
                        {
                            registers[register] -= value;
                        }
                    }
                }

                if(registers.Values.Max() > max)
                {
                    max = registers.Values.Max();
                }
            }
            return registers.Values.Max().ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input8.txt").ToList();
            return max.ToString();
        }
    }
}

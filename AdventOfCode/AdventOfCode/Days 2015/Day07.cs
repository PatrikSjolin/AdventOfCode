using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day07 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input07.txt").ToList();
            //List<string> inputLines = new List<string> { ""}
            Dictionary<string, int> registers = new Dictionary<string, int>();
            
            foreach(var s in inputLines)
            {
                if (s.Contains("RSHIFT"))
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[0];
                    string reg2 = split[4];
                    AddRegsIfNotExist(registers, reg1);
                    AddRegsIfNotExist(registers, reg2);

                    registers[reg2] = registers[reg1] / (int)Math.Pow(2, int.Parse(split[2].ToString()));
                }
                else if (s.Contains("LSHIFT"))
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[0];
                    string reg2 = split[4];
                    AddRegsIfNotExist(registers, reg1);
                    AddRegsIfNotExist(registers, reg2);

                    registers[reg2] = registers[reg1] * (int)Math.Pow(2, int.Parse(split[2].ToString()));
                }
                else if (s.Contains("OR"))
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[0];
                    string reg2 = split[2];
                    AddRegsIfNotExist(registers, reg1);
                    AddRegsIfNotExist(registers, reg2);
                    registers[split[4]] = registers[reg1] | registers[reg2];
                }
                else if (s.Contains("NOT"))
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[1];
                    string reg2 = split[3];
                    AddRegsIfNotExist(registers, reg1);
                    AddRegsIfNotExist(registers, reg2);
                    registers[reg2] = ~registers[reg1];
                }
                else if (s.Contains("AND"))
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[0];
                    string reg2 = split[2];
                    AddRegsIfNotExist(registers, reg1);
                    AddRegsIfNotExist(registers, reg2);
                    registers[split[4]] = registers[reg1] & registers[reg2];
                }
                else
                {
                    List<string> split = s.Split(' ').ToList();
                    string reg1 = split[0];
                    string reg2 = split[2];
                    UInt16 result = 0;

                    AddRegsIfNotExist(registers, reg2);
                    if (UInt16.TryParse(reg1, out result))
                    {
                        registers[reg2] = result;
                    }
                    else
                    {
                        AddRegsIfNotExist(registers, reg1);
                        registers[reg2] = registers[reg1];
                    }
                }
            }
            return "";
        }

        private void AddRegsIfNotExist(Dictionary<string, int> registers, string reg1)
        {
            if (!registers.ContainsKey(reg1))
                registers.Add(reg1,0);
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}

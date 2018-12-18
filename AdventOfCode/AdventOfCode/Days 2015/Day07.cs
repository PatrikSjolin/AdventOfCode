using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day07 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input07.txt").ToList();
            //List<string> inputLines = new List<string> { ""}
            Dictionary<string, int> registers = new Dictionary<string, int>();

            while (inputLines.Count > 0)
            {
                foreach (var s in inputLines.ToList())
                {
                    if (s.Contains("RSHIFT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[4];
                        if(registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);

                            registers[reg2] = registers[reg1] / (int)Math.Pow(2, int.Parse(split[2].ToString()));
                            inputLines.Remove(s);
                        }
                    }
                    else if (s.Contains("LSHIFT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[4];

                        if (registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);
                            registers[reg2] = registers[reg1] * (int)Math.Pow(2, int.Parse(split[2].ToString()));
                            inputLines.Remove(s);
                        }
                    }
                    else if (s.Contains("OR"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];
                        int test1 = 0;
                        if(!int.TryParse(reg1, out test1))
                        {
                            if (registers.ContainsKey(reg1) && registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = registers[reg1] | registers[reg2];
                                inputLines.Remove(s);
                            }
                        }
                        else
                        {
                            if (registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = test1 | registers[reg2];
                                inputLines.Remove(s);
                            }
                        }



                        //AddRegsIfNotExist(registers, reg1);
                        //AddRegsIfNotExist(registers, reg2);

                        //if (registers[split[4]] < 0)
                        //{
                        //    registers[split[4]] += UInt16.MaxValue;
                        //}
                    }
                    else if (s.Contains("AND"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];

                        int test1 = 0;
                        if (!int.TryParse(reg1, out test1))
                        {
                            if (registers.ContainsKey(reg1) && registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = registers[reg1] & registers[reg2];
                                inputLines.Remove(s);
                            }
                        }
                        else
                        {
                            if (registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = test1 & registers[reg2];
                                inputLines.Remove(s);
                            }
                        }

                        //if (registers[split[4]] < 0)
                        //{
                        //    registers[split[4]] += UInt16.MaxValue + 1;
                        //}
                    }
                    else if (s.Contains("NOT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[1];
                        string reg2 = split[3];

                        if (registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);
                            registers[reg2] = ~registers[reg1];
                            if (registers[reg2] < 0)
                            {
                                registers[reg2] += UInt16.MaxValue + 1;
                            }
                            inputLines.Remove(s);
                        }
                        //AddRegsIfNotExist(registers, reg1);
                        
                    }
                    else
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];
                        UInt16 result = 0;

                        //AddRegsIfNotExist(registers, reg2);
                        if (UInt16.TryParse(reg1, out result))
                        {
                            registers[reg2] = result;
                            inputLines.Remove(s);
                        }
                        else
                        {
                            if(registers.ContainsKey(reg1))
                            {
                                AddRegsIfNotExist(registers, reg2);
                                registers[reg2] = registers[reg1];
                                inputLines.Remove(s);
                            }
                        }
                    }
                }
            }

            previousA = registers["a"];
            return registers["a"].ToString();
        }

        int previousA = 0;

        private void AddRegsIfNotExist(Dictionary<string, int> registers, string reg1)
        {
            if (!registers.ContainsKey(reg1))
                registers.Add(reg1,0);
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input07.txt").ToList();
            //List<string> inputLines = new List<string> { ""}
            Dictionary<string, int> registers = new Dictionary<string, int>();
            registers["b"] = previousA;
            inputLines.Remove("1674 -> b");
            while (inputLines.Count > 0)
            {
                foreach (var s in inputLines.ToList())
                {
                    if (s.Contains("RSHIFT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[4];
                        if (registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);

                            registers[reg2] = registers[reg1] / (int)Math.Pow(2, int.Parse(split[2].ToString()));
                            inputLines.Remove(s);
                        }
                    }
                    else if (s.Contains("LSHIFT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[4];

                        if (registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);
                            registers[reg2] = registers[reg1] * (int)Math.Pow(2, int.Parse(split[2].ToString()));
                            inputLines.Remove(s);
                        }
                    }
                    else if (s.Contains("OR"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];
                        int test1 = 0;
                        if (!int.TryParse(reg1, out test1))
                        {
                            if (registers.ContainsKey(reg1) && registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = registers[reg1] | registers[reg2];
                                inputLines.Remove(s);
                            }
                        }
                        else
                        {
                            if (registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = test1 | registers[reg2];
                                inputLines.Remove(s);
                            }
                        }



                        //AddRegsIfNotExist(registers, reg1);
                        //AddRegsIfNotExist(registers, reg2);

                        //if (registers[split[4]] < 0)
                        //{
                        //    registers[split[4]] += UInt16.MaxValue;
                        //}
                    }
                    else if (s.Contains("AND"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];

                        int test1 = 0;
                        if (!int.TryParse(reg1, out test1))
                        {
                            if (registers.ContainsKey(reg1) && registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = registers[reg1] & registers[reg2];
                                inputLines.Remove(s);
                            }
                        }
                        else
                        {
                            if (registers.ContainsKey(reg2))
                            {
                                registers[split[4]] = test1 & registers[reg2];
                                inputLines.Remove(s);
                            }
                        }

                        //if (registers[split[4]] < 0)
                        //{
                        //    registers[split[4]] += UInt16.MaxValue + 1;
                        //}
                    }
                    else if (s.Contains("NOT"))
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[1];
                        string reg2 = split[3];

                        if (registers.ContainsKey(reg1))
                        {
                            AddRegsIfNotExist(registers, reg2);
                            registers[reg2] = ~registers[reg1];
                            if (registers[reg2] < 0)
                            {
                                registers[reg2] += UInt16.MaxValue + 1;
                            }
                            inputLines.Remove(s);
                        }
                        //AddRegsIfNotExist(registers, reg1);

                    }
                    else
                    {
                        List<string> split = s.Split(' ').ToList();
                        string reg1 = split[0];
                        string reg2 = split[2];
                        UInt16 result = 0;

                        //AddRegsIfNotExist(registers, reg2);
                        if (UInt16.TryParse(reg1, out result))
                        {
                            registers[reg2] = result;
                            inputLines.Remove(s);
                        }
                        else
                        {
                            if (registers.ContainsKey(reg1))
                            {
                                AddRegsIfNotExist(registers, reg2);
                                registers[reg2] = registers[reg1];
                                inputLines.Remove(s);
                            }
                        }
                    }
                }
            }
            return registers["a"].ToString();
        }
    }
}

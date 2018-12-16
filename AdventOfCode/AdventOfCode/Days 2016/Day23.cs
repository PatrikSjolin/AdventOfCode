using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2016
{
    public class Day23 : IPuzzle
    {
        private int taskNumber = 23;

        public bool Active => false;

        public List<string> ReadInput(string fileName)
        {
            string path = string.Format("../../Tasks/{0}/{1}.txt", taskNumber, fileName);

            List<string> inputList = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                inputList.Add(line);
            }

            return inputList;
        }

        private void RunOperations(List<string> input, Dictionary<string, int> registers)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].StartsWith("cpy"))
                {
                    List<string> copy = input[i].Split(' ').ToList();
                    int no = 0;
                    if(int.TryParse(copy[2], out no))
                    {
                        continue;
                    }
                    
                    int number = 0;
                    if (int.TryParse(copy[1], out number))
                    {
                        registers[copy[2]] = number;
                    }
                    else
                    {
                        registers[copy[2]] = registers[copy[1]];
                    }
                }
                else if (input[i].StartsWith("inc"))
                {
                    List<string> inc = input[i].Split(' ').ToList();
                    registers[inc[1]]++;
                }
                else if (input[i].StartsWith("dec"))
                {
                    List<string> inc = input[i].Split(' ').ToList();
                    registers[inc[1]]--;
                }
                else if (input[i].StartsWith("jnz"))
                {
                    List<string> jnz = input[i].Split(' ').ToList();
                    int number;
                    if (int.TryParse(jnz[1], out number))
                    {
                        if (number != 0)
                        {
                            int val;
                            if(!int.TryParse(jnz[2], out val))
                            {
                                val = registers[jnz[2].ToString()];
                            }
                            
                            i += val - 1;
                        }
                    }
                    else if (registers[jnz[1]] != 0)
                    {
                        i += int.Parse(jnz[2]) - 1;
                    }
                }
                else if (input[i].StartsWith("mlt"))
                {
                    List<string> mlt = input[i].Split(' ').ToList();

                    int a = registers[mlt[1]];
                    int b = registers[mlt[2]];

                    a = a * b;

                    registers[mlt[2]] = a;
                }
                else if (input[i].StartsWith("tgl"))
                {
                    List<string> tgl = input[i].Split(' ').ToList();

                    int registerValue = registers[tgl[1]];

                    int index = i + registerValue;

                    if(index >= input.Count)
                    {
                        continue;
                    }

                    string operation = input[index];

                    List<string> op = operation.Split(' ').ToList();

                    if(op.Count == 2)
                    {
                        if (op[0].StartsWith("inc"))
                        {
                            input[index] = "dec " + op[1];
                        }
                        else
                        {
                            input[index] = "inc " + op[1];
                        }
                    }
                    else if(op.Count == 3)
                    {
                        if (op[0].StartsWith("jnz"))
                        {
                            input[index] = "cpy " + op[1] + " " + op[2];   
                        }
                        else
                        {
                            input[index] = "jnz " + op[1] + " " + op[2];
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void GoOne()
        {
            var instructions = ReadInput("input");
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 7 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
            RunOperations(instructions, registers);
            Console.WriteLine(registers["a"]);
        }

        public void GoTwo()
        {
            var instructions = ReadInput("inputTest2");
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 12 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
            RunOperations(instructions, registers);
            Console.WriteLine(registers["a"]);
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

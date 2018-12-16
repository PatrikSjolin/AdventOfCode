using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day12 : IPuzzle
    {
        private int taskNumber = 12;

        public bool Active => false;

        public List<string> ReadInput()
        {
            string path = string.Format("../../Tasks/{0}/input.txt", taskNumber);

            List<string> inputList = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                inputList.Add(line);
            }

            return inputList;
        }

        public void GoOne()
        {
            List<string> input = ReadInput();
            var numberStored = RunOperations(input);
            Console.WriteLine(numberStored["a"]);
        }

        private Dictionary<string, int> RunOperations(List<string> input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
            for(int i = 0; i < input.Count; i++)
            {
                if (input[i].StartsWith("cpy"))
                {
                    List<string> copy = input[i].Split(' ').ToList();

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
                    if(int.TryParse(jnz[1], out number))
                    {
                        if( number != 0)
                        {
                            i += int.Parse(jnz[2]) - 1;
                        }
                    }
                    else if(registers[jnz[1]] != 0)
                    {
                        i += int.Parse(jnz[2]) - 1;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            return registers;
        }

        public void GoTwo()
        {
            List<string> input = ReadInput();
            var numberStored = RunOperations2(input);
            Console.WriteLine(numberStored["a"]);
        }

        private Dictionary<string, int> RunOperations2(List<string> input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } };
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].StartsWith("cpy"))
                {
                    List<string> copy = input[i].Split(' ').ToList();

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
                            i += int.Parse(jnz[2]) - 1;
                        }
                    }
                    else if (registers[jnz[1]] != 0)
                    {
                        i += int.Parse(jnz[2]) - 1;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            return registers;
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

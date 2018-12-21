using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day21 : IPuzzle
    {
        public bool Active => true;



        private Dictionary<long, long> Addr(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] + registers[b];
            return registers;
        }

        private Dictionary<long, long> Addi(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] + b;
            return registers;
        }

        private Dictionary<long, long> Mulr(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] * registers[b];
            return registers;
        }

        private Dictionary<long, long> Muli(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] * b;
            return registers;
        }

        private Dictionary<long, long> Banr(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] & registers[b];
            return registers;
        }

        private Dictionary<long, long> Bani(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] & b;
            return registers;
        }

        private Dictionary<long, long> Borr(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] | registers[b];
            return registers;
        }

        private Dictionary<long, long> Bori(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a] | b;
            return registers;
        }

        private Dictionary<long, long> Setr(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = registers[a];
            return registers;
        }

        private Dictionary<long, long> Seti(Dictionary<long, long> registers, long a, long b, long c)
        {
            registers[c] = a;
            return registers;
        }

        private Dictionary<long, long> Gtir(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (a > registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<long, long> Gtri(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (registers[a] > b)
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<long, long> Gtrr(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (registers[a] > registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<long, long> Eqir(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (a == registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<long, long> Eqri(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (registers[a] == b)
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<long, long> Eqrr(Dictionary<long, long> registers, long a, long b, long c)
        {
            if (registers[a] == registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        public string RunOne()
        {
            List<long> numbers = new List<long>();
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input21.txt").ToList();

            long instructionPointerRegister = long.Parse(inputLines[0].Split(' ')[1]);
            long instructionPointer = 0;

            inputLines.RemoveAt(0);

            Dictionary<long, long> registers = new Dictionary<long, long>();

            registers.Clear();
            registers.Add(0, 0);
            registers.Add(1, 0);
            registers.Add(2, 0);
            registers.Add(3, 0);
            registers.Add(4, 0);
            registers.Add(5, 0);
            int count = 0;

            long highest = 0;

            while (instructionPointer >= 0 && instructionPointer < inputLines.Count)
            {

                List<string> instruction = inputLines[(int)instructionPointer].Split(' ').ToList();

                if (instruction[0] == "addi")
                {
                    Addi(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "addr")
                {
                    Addr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "seti")
                {
                    Seti(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "setr")
                {
                    Setr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "muli")
                {
                    Muli(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "mulr")
                {
                    Mulr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "gtir")
                {
                    Gtir(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "gtrr")
                {
                    Gtrr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "eqri")
                {
                    Eqri(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "eqrr")
                {
                    Eqrr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "bani")
                {
                    Bani(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "bori")
                {
                    Bori(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }

                if(instructionPointer == 28)
                {
                    return registers[4].ToString();
                }

                instructionPointer = registers[instructionPointerRegister];
                instructionPointer++;
                registers[instructionPointerRegister] = instructionPointer;
                
            }

            return "";
        }

        public string RunTwo()
        {
            List<long> numbers = new List<long>();
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input21.txt").ToList();

            long instructionPointerRegister = long.Parse(inputLines[0].Split(' ')[1]);
            long instructionPointer = 0;

            inputLines.RemoveAt(0);

            Dictionary<long, long> registers = new Dictionary<long, long>();

            registers.Clear();
            registers.Add(0, 0);
            registers.Add(1, 0);
            registers.Add(2, 0);
            registers.Add(3, 0);
            registers.Add(4, 0);
            registers.Add(5, 0);

            while (instructionPointer >= 0 && instructionPointer < inputLines.Count)
            {
                List<string> instruction = inputLines[(int)instructionPointer].Split(' ').ToList();

                if (instruction[0] == "addi")
                {
                    Addi(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "addr")
                {
                    Addr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "seti")
                {
                    Seti(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "setr")
                {
                    Setr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "muli")
                {
                    Muli(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "mulr")
                {
                    Mulr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "gtir")
                {
                    Gtir(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "gtrr")
                {
                    Gtrr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "eqri")
                {
                    Eqri(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "eqrr")
                {
                    Eqrr(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "bani")
                {
                    Bani(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }
                else if (instruction[0] == "bori")
                {
                    Bori(registers, long.Parse(instruction[1]), long.Parse(instruction[2]), long.Parse(instruction[3]));
                }

                if (instructionPointer == 28)
                {
                    if (numbers.Contains(registers[4]))
                    {
                        return numbers.Last().ToString();
                    }
                    else
                    {
                        numbers.Add(registers[4]);
                    }
                }
                instructionPointer = registers[instructionPointerRegister];
                instructionPointer++;
                registers[instructionPointerRegister] = instructionPointer;
            }

            return "";
        }
    }
}
